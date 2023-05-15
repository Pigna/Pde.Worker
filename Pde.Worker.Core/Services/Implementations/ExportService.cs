using Pde.Worker.Core.Contracts;
using Pde.Worker.Core.Models;
using Pde.Worker.Data.Database;
using Pde.Worker.Data.Models;

namespace Pde.Worker.Core.Services.Implementations;

public class ExportService : IExportService
{
    private readonly IFakeDataService _fakeDataService;
    private readonly IDbExportProvider _provider;
    private readonly IFileWriterService _fileWriterService;

    public ExportService(IDbExportProvider provider, IFakeDataService fakeDataService, IFileWriterService fileWriterService)
    {
        _fakeDataService = fakeDataService;
        _provider = provider;
        _fileWriterService = fileWriterService;
    }

    public SubmitExportResponse SubmitExportData(SubmitExportDataRequest request)
    {
        const int stepSize = 2;
        var combined = request.ExportDataViewModels.GroupBy(item => item.TableName).ToList();
        var tableDataResult = new List<TableData>();
        
        //Write SQL db create file
        _fileWriterService.CreateTables(request.ExportDataViewModels, request.DatabaseConnectionInfo.Database);

        foreach (var table in combined)
        {
            var columns = table.Where(item => item.DataType == FakeDataType.Null)
                .Select(item => item.ColumnName)
                .ToList();
            
            var columnsToAdd = table.Where(item => item.DataType != FakeDataType.Null)
                .ToList();
            
            var progression = 0;
            while (true)
            {
                //Grab a data batch
                var tableData = _provider.FetchTableData(
                        table.Key,
                        columns,
                        request.DatabaseConnectionInfo.Username,
                        request.DatabaseConnectionInfo.Password,
                        request.DatabaseConnectionInfo.Host,
                        request.DatabaseConnectionInfo.Port,
                        request.DatabaseConnectionInfo.Database,
                        progression,
                        stepSize)
                    .Result;

                InsertFakeData(columnsToAdd, tableData);

                //Write data import script to file
                _fileWriterService.InsertData(tableData, request.DatabaseConnectionInfo.Database);
                
                if (tableData.Data != null && tableData.Data.Count < stepSize)
                {
                    break;
                }

                progression += stepSize;
            }
        }
        return new SubmitExportResponse
        {
            Result = SubmitExportResult.Success,
            Value = tableDataResult
        };
    }

    private void InsertFakeData(List<ExportDataViewModel> columnsToAdd, TableData tableData)
    {
        if (columnsToAdd.Count <= 0) return;
        foreach (var row in tableData.Data!)
        {
            //TODO: What to do if a seed is a string?
            var firstValue = row!.FirstOrDefault().Value;
            var seed = Convert.ToInt32(firstValue);
            foreach (var column in columnsToAdd)
                row?.Add(column.ColumnName, _fakeDataService.GetFakerDataByType(column.DataType, seed));
        }
    }
}
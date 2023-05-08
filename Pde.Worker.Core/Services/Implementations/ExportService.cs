using Pde.Worker.Core.Contracts;
using Pde.Worker.Core.Models;
using Pde.Worker.Data.Database;
using Pde.Worker.Data.Models;

namespace Pde.Worker.Core.Services.Implementations;

public class ExportService : IExportService
{
    private readonly IFakeDataService _fakeDataService;
    private readonly IDbExportProvider _provider;

    public ExportService(IDbExportProvider provider, IFakeDataService fakeDataService)
    {
        _fakeDataService = fakeDataService;
        _provider = provider;
    }

    public SubmitExportResponse SubmitExportData(SubmitExportDataRequest request)
    {
        var combined = request.ExportDataViewModels.GroupBy(item => item.TableName).ToList();
        var tableDataResult = new List<TableData>();

        foreach (var table in combined)
        {
            //TODO: Improve this process by iterating over a X amount of data lines
            var columns = table.Where(item => item.DataType == FakeDataType.Null)
                .Select(item => item.ColumnName)
                .ToList();
            var tableData = _provider.FetchTableData(
                    table.Key,
                    columns,
                    request.DatabaseConnectionInfo.Username,
                    request.DatabaseConnectionInfo.Password,
                    request.DatabaseConnectionInfo.Host,
                    request.DatabaseConnectionInfo.Port,
                    request.DatabaseConnectionInfo.Database)
                .Result;

            var columnsToAdd = table.Where(item => item.DataType != FakeDataType.Null)
                .ToList();

            //TODO: Check if there is actual data to insert.
            foreach (var row in tableData.Data)
            {
                //TODO: What to do if a seed is a string?
                var firstValue = row!.FirstOrDefault().Value;
                var seed = Convert.ToInt32(firstValue);
                foreach (var column in columnsToAdd)
                    row?.Add(column.ColumnName, _fakeDataService.GetFakerDataByType(column.DataType, seed));
            }

            tableDataResult.Add(tableData);
        }

        Console.WriteLine("Job's done!");
        return new SubmitExportResponse
        {
            Result = SubmitExportResult.Success,
            Value = tableDataResult
        };
    }
}
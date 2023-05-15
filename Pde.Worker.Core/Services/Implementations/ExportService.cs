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
            var columns = table.Where(item => item.DataType == FakeDataType.Null)
                .Select(item => item.ColumnName)
                .ToList();
            
            var columnsToAdd = table.Where(item => item.DataType != FakeDataType.Null)
                .ToList();
            
            var progression = 0;
            //Request database size
            //For(var i = 0; i<size; i++)
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
                        progression)
                    .Result;
                Console.WriteLine(table.Key + " -> Got a data batch");

                if (columnsToAdd.Count > 0)
                {
                    //Insert fake data
                    foreach (var row in tableData.Data)
                    {
                        //TODO: What to do if a seed is a string?
                        var firstValue = row!.FirstOrDefault().Value;
                        var seed = Convert.ToInt32(firstValue);
                        foreach (var column in columnsToAdd)
                            row?.Add(column.ColumnName, _fakeDataService.GetFakerDataByType(column.DataType, seed));
                    }
                }
                Console.WriteLine(table.Key + " -> Filled a data batch with fake data");
                
                //TODO: --Write to file
                tableDataResult.Add(tableData);
                if (tableData.Data.Count() < 2)
                {
                    Console.WriteLine(table.Key + " -> Table is done");
                    break;
                }

                progression += 2;
            }
        }

        Console.WriteLine("Job's done!");
        return new SubmitExportResponse
        {
            Result = SubmitExportResult.Success,
            Value = tableDataResult
        };
    }
}
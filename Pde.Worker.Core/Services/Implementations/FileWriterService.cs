using System.Text;
using Pde.Worker.Core.Models;
using Pde.Worker.Data.Models;

namespace Pde.Worker.Core.Services.Implementations;

internal class FileWriterService : IFileWriterService
{
    public void CreateTables(IEnumerable<ExportDataViewModel> exportData, string projectName)
    {
        var fileName = $"{projectName}.sql";

        var tables = exportData.GroupBy(table => table.TableName);

        var scriptBuilder = new StringBuilder();
        foreach (var table in tables)
        {
            scriptBuilder.Append($"CREATE TABLE \"{table.Key}\" (");
            scriptBuilder.AppendJoin(", ", table.Select(column => $"\"{column.ColumnName}\" {column.DataType}"));
            scriptBuilder.AppendLine(");");
        }
        File.WriteAllText(fileName, scriptBuilder.ToString());
    }

    public void InsertData(TableData exportData, string projectName)
    {
        if (exportData.Data == null) return;
        
        var fileName = $"{projectName}.sql";

        using var writer = File.AppendText(fileName);
        foreach (var row in exportData.Data)
        {
            var columns = string.Join(", ", row.Select(column => $"\"{column.Key}\""));
            var data = string.Join(", ", row.Select(column => $"\"{column.Value}\""));

            writer.WriteLine($"INSERT INTO {exportData.TableName} ({columns}) VALUES ({data});");
            
        }
    }

    public void AddConstraints(object data, string projectName)
    {
        throw new NotImplementedException();
    }
}





















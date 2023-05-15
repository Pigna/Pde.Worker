using System.Text;
using Pde.Worker.Core.Models;
using Pde.Worker.Data.Models;

namespace Pde.Worker.Core.Services.Implementations;

internal static class Exporter
{
    public static void CreateTables(IEnumerable<ExportDataViewModel> exportData)
    {
        const string fileName = "ProjectName_Tables.sql";

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

    public static void InsertData(TableData exportData)
    {
        if (exportData.Data == null) return;
        
        var fileName = $"ProjectName_Insert_{exportData.TableName}.sql";
        
        using var writer = new StreamWriter(fileName);
        foreach (var row in exportData.Data)
        {
            var columns = string.Join(", ", row.Select(column => $"\"{column.Key}\""));
            var data = string.Join(", ", row.Select(column => $"\"{column.Value}\""));

            writer.WriteLine($"INSERT INTO {exportData.TableName} ({columns}) VALUES ({data});");
        }
    }
}





















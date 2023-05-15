using Dapper;
using Pde.Worker.Data.Models;

namespace Pde.Worker.Data.Database.Implementations;

public class PostgresExportProvider : IDbExportProvider
{
    private readonly IDbConnectionFactory _connectionFactory;

    public PostgresExportProvider(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<TableData> FetchTableData(
        string tableName,
        ICollection<string> columns,
        string username,
        string password,
        string host,
        string port,
        string database,
        int offset,
        int stepSize
        )
    {
        var sqlSelectQuery = CreateSqlQuery(tableName, columns, offset, stepSize);
        var connectionString = _connectionFactory.CreateConnectionString(username, password, host, port, database);
        using var databaseConnection = _connectionFactory.Connect(connectionString);
        var queryResult = (await databaseConnection.QueryAsync<dynamic>(sqlSelectQuery)).ToList();

        var data = new List<IDictionary<string, object>>();

        foreach (var row in queryResult)
        {
            var item = row as IDictionary<string, object>;
            data.Add(item!);
        }

        var tableData = new TableData
        {
            TableName = tableName,
            Data = data
        };
        return tableData;
    }

    private static string CreateSqlQuery(string tableName, ICollection<string> columns, int offset, int stepSize)
    {
        var preparedColumns = string.Join(",", columns.Select(columnName => $"\"{columnName}\""));
        var selectQuery = @$"SELECT {preparedColumns} FROM ""{tableName}"" ORDER BY CURRENT_TIMESTAMP OFFSET {offset} ROWS FETCH NEXT {stepSize} ROWS ONLY;";
        return selectQuery;
    }
}
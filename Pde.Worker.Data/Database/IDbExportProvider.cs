using Pde.Worker.Data.Models;

namespace Pde.Worker.Data.Database;

public interface IDbExportProvider
{
    Task<TableData> FetchTableData(string tableName,
        ICollection<string> columns,
        string username,
        string password,
        string host,
        string port,
        string database);
}
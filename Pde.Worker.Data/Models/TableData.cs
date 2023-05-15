namespace Pde.Worker.Data.Models;

public class TableData
{
    public string TableName { get; set; } = null!;
    public IList<IDictionary<string, object>>? Data { get; set; }
}
using Pde.Worker.Data.Models;

namespace Pde.Worker.Core.Contracts;

public class SubmitExportResponse
{
    public SubmitExportResult Result { get; set; }
    public IList<TableData>? Value { get; set; }
}
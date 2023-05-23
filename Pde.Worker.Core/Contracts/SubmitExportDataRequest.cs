using Pde.Worker.Core.Models;

namespace Pde.Worker.Core.Contracts;

public class SubmitExportDataRequest
{
    public DatabaseConnectionInfoViewModel DatabaseConnectionInfo { get; set; } = null!;
    public ICollection<ExportDataViewModel> ExportDataViewModels { get; set; } = null!;
    public ICollection<TableRelationViewModel> TableRelationViewModels { get; set; } = null!;
}
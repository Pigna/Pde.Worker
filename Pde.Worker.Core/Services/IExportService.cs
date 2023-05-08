using Pde.Worker.Core.Contracts;

namespace Pde.Worker.Core.Services;

public interface IExportService
{
    SubmitExportResponse SubmitExportData(SubmitExportDataRequest request);
}
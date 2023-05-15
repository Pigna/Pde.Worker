using Pde.Worker.Core.Models;
using Pde.Worker.Data.Models;

namespace Pde.Worker.Core.Services;

public interface IFileWriterService
{
    void CreateTables(IEnumerable<ExportDataViewModel> exportData, string projectName);
    void InsertData(TableData exportData, string projectName);
    void AddConstraints(object data, string projectName);
}
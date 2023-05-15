using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pde.Worker.Core.Services;
using Pde.Worker.Core.Services.Implementations;
using Pde.Worker.Data.Extensions;

namespace Pde.Worker.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPdeWorkerCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IFakeDataService, FakeDataService>();
        services.AddTransient<IExportService, ExportService>();
        services.AddTransient<IFileWriterService, FileWriterService>();

        services.AddDependencies(configuration);
    }

    private static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPdeWorkerDataServices(configuration);
    }
}
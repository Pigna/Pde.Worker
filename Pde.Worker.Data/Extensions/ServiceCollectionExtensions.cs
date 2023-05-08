using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pde.Worker.Data.Database;
using Pde.Worker.Data.Database.Implementations;

namespace Pde.Worker.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPdeWorkerDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDbExportProvider, PostgresExportProvider>();
        services.AddTransient<IDbConnectionFactory, PostgresConnectionFactory>();
        services.AddDependencies(configuration);
    }

    private static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
    }
}
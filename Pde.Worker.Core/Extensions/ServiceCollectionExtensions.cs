using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pde.Worker.Core.Services;
using Pde.Worker.Core.Services.Implementations;

namespace Pde.Worker.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPdeWorkerCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITestService, TestService>();

        services.AddDependencies(configuration);
    }

    private static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddPdeWorkerCoreServices(configuration);
    }
}
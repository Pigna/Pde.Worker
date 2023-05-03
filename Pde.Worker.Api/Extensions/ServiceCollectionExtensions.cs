using Hangfire;
using Hangfire.PostgreSql;
using Pde.Worker.Core.Extensions;

namespace Pde.Worker.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPdeWorkerApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //Add hangfire with postgres db storage
        services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeResolver()
            .UsePostgreSqlStorage("Host=localhost;Port=32768;Username=postgres;Password=postgrespw;Database=postgres;"));

        //Add hangfire server
        services.AddHangfireServer();
        
        services.AddDependencies(configuration);
    }

    private static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPdeWorkerCoreServices(configuration);
    }
}
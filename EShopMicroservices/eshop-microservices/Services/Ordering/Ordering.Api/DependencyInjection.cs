namespace Ordering.Api;

public static class DependencyInjection
    {
    public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
        services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
        return services;
        }
    public static WebApplication UseApiServices(this WebApplication application)
        {
        application.MapCarter();
        return application;
        }
    }


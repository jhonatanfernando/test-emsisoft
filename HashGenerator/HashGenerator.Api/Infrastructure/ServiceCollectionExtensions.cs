using HashGenerator.Core.Repositories;
using HashGenerator.Core.Services;
using HashGenerator.Data.Repositories;
using HashGenerator.Service.Services;

namespace HashGenerator.Api.Infrastructure;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IHashRepository, HashRepository>();
    }

    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IHashService, HashService>();
    }
}


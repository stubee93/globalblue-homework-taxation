using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taxation.Application.Abstractions;
using Taxation.Infrastructure.Adapters;
using Taxation.Infrastructure.Options;

namespace Taxation.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions(configuration);

        services.AddSingleton<IVatRateCatalog, ConfigVatRateCatalog>();

        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var vatOptionsList = configuration
            .GetSection("VatOptions")
            .Get<IReadOnlyCollection<VatOptions>>() ?? [];

        services.AddSingleton(vatOptionsList);

        return services;
    }
}
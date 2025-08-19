using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taxation.Api.Middleware.ExceptionHandlers;

namespace Taxation.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddExceptionHandler<ValidationExceptionHandler>()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(
            c =>
            {
                var apiXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXml);
                if (File.Exists(apiXmlPath))
                    c.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
            });
        services.AddProblemDetails();

        return services;
    }
}
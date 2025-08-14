using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taxation.Application.Behaviors;
using Taxation.Application.UseCases.CalculateVat;
using Taxation.Domain.Services;

namespace Taxation.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IVatCalculator, VatCalculator>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CalculateVatCommandHandler).Assembly));
        
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        services.AddValidatorsFromAssemblyContaining<CalculateVatCommandValidator>();
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); 

        return services;
    }
    
}
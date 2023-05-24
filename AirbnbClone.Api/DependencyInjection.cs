using AirbnbClone.Api.Common.Mapping;
using AirbnbClone.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AirbnbClone.Api;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, AirbnbCloneProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}

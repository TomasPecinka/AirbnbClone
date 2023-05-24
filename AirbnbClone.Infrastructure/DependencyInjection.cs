using AirbnbClone.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.DependencyInjection;
using AirbnbClone.Infrastructure.Authentication;
using AirbnbClone.Application.Common.Interfaces.Services;
using AirbnbClone.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using AirbnbClone.Infrastructure.Persistence;
using AirbnbClone.Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace AirbnbClone.Infrastructure;

public static class DependencyInjection
{
     public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configration)
    {
        services.AddAuth(configration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configration)
    {
        var jwtSettings = new JwtSettings();
        configration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}

using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
        return services;
    }
}
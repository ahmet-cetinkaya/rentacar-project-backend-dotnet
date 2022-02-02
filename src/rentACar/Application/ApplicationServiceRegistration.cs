using System.Reflection;
using Application.Features.Brands.Rules;
using Application.Features.Cars.Rules;
using Application.Features.Models.Rules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<BrandBusinessRules>();
        services.AddScoped<CarBusinessRules>();
        services.AddScoped<ModelBusinessRules>();
        return services;
    }
}
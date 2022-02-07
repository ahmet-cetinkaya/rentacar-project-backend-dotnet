using System.Reflection;
using Application.Features.Brands.Rules;
using Application.Features.Cars.Rules;
using Application.Features.Colors.Rules;
using Application.Features.Customers.Rules;
using Application.Features.Fuels.Rules;
using Application.Features.Models.Rules;
using Application.Features.Rentals.Rules;
using Application.Features.Transmissions.Rules;
using Core.Application.Pipelines.Validation;
using FluentValidation;
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
        services.AddScoped<ColorBusinessRules>();
        services.AddScoped<CustomerBusinessRules>();
        services.AddScoped<FuelBusinessRules>();
        services.AddScoped<ModelBusinessRules>();
        services.AddScoped<RentalBusinessRules>();
        services.AddScoped<TransmissionBusinessRules>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        return services;
    }
}
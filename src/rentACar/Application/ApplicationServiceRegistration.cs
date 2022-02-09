using System.Reflection;
using Application.Features.Brands.Rules;
using Application.Features.CarDamages.Rules;
using Application.Features.Cars.Rules;
using Application.Features.Colors.Rules;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.Customers.Rules;
using Application.Features.FindeksCreditRates.Rules;
using Application.Features.Fuels.Rules;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Rules;
using Application.Features.Models.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Features.Rentals.Rules;
using Application.Features.Transmissions.Rules;
using Application.Features.Users.Rules;
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
        services.AddScoped<CarDamageBusinessRules>();
        services.AddScoped<ColorBusinessRules>();
        services.AddScoped<CorporateCustomerBusinessRules>();
        services.AddScoped<CustomerBusinessRules>();
        services.AddScoped<FindeksCreditRateBusinessRules>();
        services.AddScoped<FuelBusinessRules>();
        services.AddScoped<IndividualCustomerBusinessRules>();
        services.AddScoped<InvoiceBusinessRules>();
        services.AddScoped<ModelBusinessRules>();
        services.AddScoped<RentalBusinessRules>();
        services.AddScoped<OperationClaimBusinessRules>();
        services.AddScoped<UserBusinessRules>();
        services.AddScoped<TransmissionBusinessRules>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        return services;
    }
}
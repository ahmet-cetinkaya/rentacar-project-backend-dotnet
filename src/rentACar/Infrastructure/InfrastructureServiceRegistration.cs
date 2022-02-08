using Application.Services;
using Application.Services.POSService;
using Infrastructure.Adapters.FakeFindeksCreditRateService;
using Infrastructure.Adapters.FakePOSService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFindeksCreditRateService, FakeFindeksCreditRateServiceAdapter>();
        services.AddScoped<IPOSService, FakePOSServiceAdapter>();
        return services;
    }
}
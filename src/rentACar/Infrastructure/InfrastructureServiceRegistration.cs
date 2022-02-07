using Application.Services;
using Infrastructure.Adapters.FakeFindeksCreditRateService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFindeksCreditRateService, FakeFindeksCreditRateServiceAdapter>();
        return services;
    }
}
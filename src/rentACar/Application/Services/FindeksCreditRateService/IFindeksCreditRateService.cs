using Domain.Entities;

namespace Application.Services.FindeksCreditRateService;

public interface IFindeksCreditRateService
{
    public Task<FindeksCreditRate> GetFindeksCreditRateByCustomerId(int customerId);
}
using Application.Services;

namespace Infrastructure.Adapters.FakeFindeksCreditRateService;

public class FakeFindeksCreditRateServiceAdapter : IFindeksCreditRateService
{
    public short GetScore(string identityNumber)
    {
        Random random = new();
        short score = Convert.ToInt16(random.Next(1900));
        return score;
    }
}
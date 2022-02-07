namespace Application.Services;

public interface IFindeksCreditRateService
{
    short GetScore(string identityNumber);
}
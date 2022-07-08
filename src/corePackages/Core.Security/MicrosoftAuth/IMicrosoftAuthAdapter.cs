namespace Core.Security.MicrosoftAuth;

public interface IMicrosoftAuthAdapter
{
    Task<MicrosoftUserDetail> GetMicrosoftUserDetail(string microsoftAccessToken);
}
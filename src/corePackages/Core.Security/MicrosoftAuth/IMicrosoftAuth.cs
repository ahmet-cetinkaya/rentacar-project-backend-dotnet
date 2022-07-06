namespace Core.Security.MicrosoftAuth;

public interface IMicrosoftAuth
{
    Task<MicrosoftUserDetail> getMicrosoftUserDetail(string microsoftAccessToken);
}
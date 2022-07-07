namespace Core.Security.GoogleAuth;

public interface IGoogleAuthAdapter
{
    Task<GoogleUserDetail> GetGoogleUserDetail(string googleAccessToken);
}
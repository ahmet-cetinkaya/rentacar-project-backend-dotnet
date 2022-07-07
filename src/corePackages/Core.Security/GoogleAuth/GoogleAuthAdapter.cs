using Google.Apis.Auth;

namespace Core.Security.GoogleAuth;

public class GoogleAuthAdapter : IGoogleAuthAdapter
{
    public async Task<GoogleUserDetail> GetGoogleUserDetail(string googleAccessToken)
    {
        GoogleJsonWebSignature.Payload? googleAuthTokenInfo =
            await GoogleJsonWebSignature.ValidateAsync(googleAccessToken);
        return new GoogleUserDetail
        {
            Email = googleAuthTokenInfo.Email,
            EmailVerified = googleAuthTokenInfo.EmailVerified,
            FirstName = googleAuthTokenInfo.GivenName,
            LastName = googleAuthTokenInfo.FamilyName
        };
    }
}
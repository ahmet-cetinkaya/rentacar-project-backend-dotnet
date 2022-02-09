using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.AuthService;

public interface IAuthService
{
    public Task<AccessToken> CreateAccessToken(User user);
}
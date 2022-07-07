using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Security.Entities;
using Core.Security.GoogleAuth;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.LoginWithGoogle;

public class LoginWithGoogleCommand : IRequest<LoggedDto>
{
    public string GoogleAccessToken { get; set; }
    public string IPAddress { get; set; }

    public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommand, LoggedDto>
    {
        private readonly IGoogleAuthAdapter _googleAuthAdapter;
        private IAuthService _authService;
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginWithGoogleCommandHandler(IGoogleAuthAdapter googleAuthAdapter, IAuthService authService,
                                             IUserService userService,
                                             AuthBusinessRules authBusinessRules)
        {
            _googleAuthAdapter = googleAuthAdapter;
            _authService = authService;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<LoggedDto> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            GoogleUserDetail googleUserDetail =
                await _googleAuthAdapter.GetGoogleUserDetail(request.GoogleAccessToken);
            await _authBusinessRules.UsersGoogleMailShouldBeVerified(googleUserDetail);

            User? user = await _userService.GetByEmail(googleUserDetail.Email);
            await _authBusinessRules.UserShouldBeExists(user);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            LoggedDto loggedDto = new()
            {
                AccessToken = createdAccessToken,
                RefreshToken = addedRefreshToken
            };
            return loggedDto;
        }
    }
}
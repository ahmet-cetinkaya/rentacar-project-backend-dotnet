using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Security.Entities;
using Core.Security.JWT;
using Core.Security.MicrosoftAuth;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Auths.Commands.LoginWithMicrosoft;

public class LoginWithMicrosoftCommand : IRequest<LoggedDto>
{
    public string MicrosoftAccessToken { get; set; }
    public string IPAddress { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginWithMicrosoftCommand, LoggedDto>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMicrosoftAuth _microsoftAuth;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IUserService userService, IAuthService authService, IMicrosoftAuth microsoftAuth,
                                   AuthBusinessRules authBusinessRules, IConfiguration configuration)
        {
            _userService = userService;
            _authService = authService;
            _microsoftAuth = microsoftAuth;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<LoggedDto> Handle(LoginWithMicrosoftCommand request, CancellationToken cancellationToken)
        {
            MicrosoftUserDetail microsoftUserDetail =
                await _microsoftAuth.getMicrosoftUserDetail(request.MicrosoftAccessToken);

            User? user = await _userService.GetByEmail(microsoftUserDetail.UserPrincipalName);
            await _authBusinessRules.UserShouldBeExists(user);

            LoggedDto loggedDto = new();
            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            loggedDto.AccessToken = createdAccessToken;
            loggedDto.RefreshToken = addedRefreshToken;
            return loggedDto;
        }
    }
}
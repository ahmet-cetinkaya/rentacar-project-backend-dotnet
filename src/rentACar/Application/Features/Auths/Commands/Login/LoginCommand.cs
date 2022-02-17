using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Login;

public class LoginCommand : IRequest<AuthenticateTokensDto>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IPAddress { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticateTokensDto>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IUserService userService, IAuthService authService,
                                   AuthBusinessRules authBusinessRules)
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<AuthenticateTokensDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetByEmail(request.UserForLoginDto.Email);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            AuthenticateTokensDto authenticateTokensDto = new()
            { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return authenticateTokensDto;
        }
    }
}
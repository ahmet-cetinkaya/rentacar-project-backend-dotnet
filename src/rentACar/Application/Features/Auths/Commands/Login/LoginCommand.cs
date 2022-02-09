using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Login;

public class LoginCommand : IRequest<AccessToken>
{
    public UserForLoginDto UserForLoginDto { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IUserRepository userRepository, IAuthService authService,
                                   AuthBusinessRules authBusinessRules)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeExists(request.UserForLoginDto.Email);

            User? user = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
            await _authBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

            AccessToken accessToken = await _authService.CreateAccessToken(user);
            return accessToken;
        }
    }
}
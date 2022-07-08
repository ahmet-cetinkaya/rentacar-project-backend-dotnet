using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Application.Services.UserService;
using Core.Security.Entities;
using Core.Security.Enums;
using MediatR;

namespace Application.Features.Auths.Commands.VerifyEmailAuthenticator;

public class VerifyEmailAuthenticatorCommand : IRequest
{
    public string ActivationKey { get; set; }

    public class VerifyEmailAuthenticatorCommandHandler : IRequestHandler<VerifyEmailAuthenticatorCommand>
    {
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;

        public VerifyEmailAuthenticatorCommandHandler(IEmailAuthenticatorRepository emailAuthenticatorRepository,
                                                      AuthBusinessRules authBusinessRules, IUserService userService)
        {
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
            _userService = userService;
        }

        public async Task<Unit> Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            EmailAuthenticator? emailAuthenticator =
                await _emailAuthenticatorRepository.GetAsync(
                    e => e.ActivationKey == request.ActivationKey);
            await _authBusinessRules.EmailAuthenticatorShouldBeExists(emailAuthenticator);
            await _authBusinessRules.EmailAuthenticatorActivationKeyShouldBeExists(emailAuthenticator);

            emailAuthenticator.ActivationKey = null;
            emailAuthenticator.IsVerified = true;
            await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

            User user = await _userService.GetById(emailAuthenticator.UserId);
            user.AuthenticatorType = AuthenticatorType.Email;
            await _userService.Update(user);

            return Unit.Value;
        }
    }
}
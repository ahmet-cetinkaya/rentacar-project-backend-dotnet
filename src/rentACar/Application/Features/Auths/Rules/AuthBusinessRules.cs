using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Auths.Rules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UserEmailShouldBeExists(string email)
    {
        User? user = await _userRepository.GetAsync(u => u.Email == email);
        if (user == null) throw new BusinessException("User mail do not exists.");
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        User? user = await _userRepository.GetAsync(u => u.Email == email);
        if (user != null) throw new BusinessException("User mail already exists.");
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        User? user = await _userRepository.GetAsync(u => u.Id == id);
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("Password don't match.");
    }
}
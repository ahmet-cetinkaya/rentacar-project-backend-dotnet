using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.Users.Rules;

public class UserBusinessRules
{
    private readonly IUserRepository _userRepository;

    public UserBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UserIdShouldExistWhenSelected(int id)
    {
        User? result = await _userRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("User not exists.");
    }
}
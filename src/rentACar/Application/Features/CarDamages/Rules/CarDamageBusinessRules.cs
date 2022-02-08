using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.CarDamages.Rules;

public class CarDamageBusinessRules
{
    private readonly ICarDamageRepository _carDamageRepository;

    public CarDamageBusinessRules(ICarDamageRepository carDamageRepository)
    {
        _carDamageRepository = carDamageRepository;
    }
 
    public async Task CarDamageIdShouldExistWhenSelected(int id)
    {
        CarDamage? result = await _carDamageRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("CarDamage not exists.");
    }
}

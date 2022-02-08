using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.CarDamages.Queries.GetByIdCarDamage;

public class GetByIdCarDamageQuery : IRequest<CarDamage>
{
    public int Id { get; set; }

    public class GetByIdCarDamageQueryHandler : IRequestHandler<GetByIdCarDamageQuery, CarDamage>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly CarDamageBusinessRules _carDamageBusinessRules;

        public GetByIdCarDamageQueryHandler(ICarDamageRepository carDamageRepository, CarDamageBusinessRules carDamageBusinessRules)
        {
            _carDamageRepository = carDamageRepository;
            _carDamageBusinessRules = carDamageBusinessRules;
        }


        public async Task<CarDamage> Handle(GetByIdCarDamageQuery request, CancellationToken cancellationToken)
        {
            await _carDamageBusinessRules.CarDamageIdShouldExistWhenSelected(request.Id);

            CarDamage? carDamage = await _carDamageRepository.GetAsync(b => b.Id == request.Id);
            return carDamage;
        }
    }
}

using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CarDamages.Commands.CreateCarDamage;

public class CreateCarDamageCommand : IRequest<CarDamage>
{
    public int CarId { get; set; }
    public string DamageDescription { get; set; }

    public class CreateCarDamageCommandHandler : IRequestHandler<CreateCarDamageCommand, CarDamage>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;
        private readonly CarDamageBusinessRules _carDamageBusinessRules;

        public CreateCarDamageCommandHandler(ICarDamageRepository carDamageRepository, IMapper mapper,
                                             CarDamageBusinessRules carDamageBusinessRules)
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<CarDamage> Handle(CreateCarDamageCommand request, CancellationToken cancellationToken)
        {
            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage createdCarDamage = await _carDamageRepository.AddAsync(mappedCarDamage);
            return createdCarDamage;
        }
    }
}
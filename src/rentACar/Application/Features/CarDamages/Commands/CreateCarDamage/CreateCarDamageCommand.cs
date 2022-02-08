using Application.Features.CarDamages.Dtos;
using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CarDamages.Commands.CreateCarDamage;

public class CreateCarDamageCommand : IRequest<CreatedCarDamageDto>
{
    public int CarId { get; set; }
    public string DamageDescription { get; set; }

    public class CreateCarDamageCommandHandler : IRequestHandler<CreateCarDamageCommand, CreatedCarDamageDto>
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

        public async Task<CreatedCarDamageDto> Handle(CreateCarDamageCommand request,
                                                      CancellationToken cancellationToken)
        {
            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage createdCarDamage = await _carDamageRepository.AddAsync(mappedCarDamage);
            CreatedCarDamageDto createdCarDamageDto = _mapper.Map<CreatedCarDamageDto>(createdCarDamage);
            return createdCarDamageDto;
        }
    }
}
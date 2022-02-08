using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CarDamages.Commands.DeleteCarDamage;

public class DeleteCarDamageCommand : IRequest<CarDamage>
{
    public int Id { get; set; }

    public class DeleteCarDamageCommandHandler : IRequestHandler<DeleteCarDamageCommand, CarDamage>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;
        private readonly CarDamageBusinessRules _carDamageBusinessRules;

        public DeleteCarDamageCommandHandler(ICarDamageRepository carDamageRepository, IMapper mapper,
                                         CarDamageBusinessRules carDamageBusinessRules)
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<CarDamage> Handle(DeleteCarDamageCommand request, CancellationToken cancellationToken)
        {
            await _carDamageBusinessRules.CarDamageIdShouldExistWhenSelected(request.Id);

            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage deletedCarDamage = await _carDamageRepository.DeleteAsync(mappedCarDamage);
            return deletedCarDamage;
        }
    }
}

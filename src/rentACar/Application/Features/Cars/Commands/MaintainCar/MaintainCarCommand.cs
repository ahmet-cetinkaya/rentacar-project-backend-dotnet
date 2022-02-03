using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Cars.Commands.MaintainCar;

public class MaintainCarCommand : IRequest<Car>
{
    public int Id { get; set; }

    public class MaintainCarCommandHandler : IRequestHandler<MaintainCarCommand, Car>
    {
        private readonly ICarRepository _carRepository;
        private readonly CarBusinessRules _carBusinessRules;

        public MaintainCarCommandHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
        }

        public async Task<Car> Handle(MaintainCarCommand request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarIdShouldExistWhenSelected(request.Id);
            await _carBusinessRules.CarCanNotBeMaintainWhenIsRented(request.Id);

            Car? car = await _carRepository.GetAsync(c => c.Id == request.Id);
            car!.CarState = CarState.Maintenance;
            await _carRepository.UpdateAsync(car);
            return car;
        }
    }
}
using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Cars.Commands.DeliverRentalCar;

public class DeliverRentalCarCommand : IRequest<Car>
{
    public int Id { get; set; }

    public class DeliverRentalCarCommandHandler : IRequestHandler<DeliverRentalCarCommand, Car>
    {
        private readonly ICarRepository _carRepository;
        private readonly CarBusinessRules _carBusinessRules;


        public DeliverRentalCarCommandHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
        }

        public async Task<Car> Handle(DeliverRentalCarCommand request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarCanNotBeRentWhenIsInMaintenance(request.Id);
            await _carBusinessRules.CarCanNotBeMaintainWhenIsRented(request.Id);

            Car? car = await _carRepository.GetAsync(c => c.Id == request.Id);
            car!.CarState = CarState.Rented;
            await _carRepository.UpdateAsync(car);
            return car;
        }
    }
}
using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Cars.Queries.GetByIdCar;

public class GetByIdCarQuery : IRequest<Car>
{
    public int Id { get; set; }

    public class GetByIdCarResponseHandler : IRequestHandler<GetByIdCarQuery, Car>
    {
        private readonly ICarRepository _carRepository;
        private readonly CarBusinessRules _carBusinessRules;

        public GetByIdCarResponseHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
        }


        public async Task<Car> Handle(GetByIdCarQuery request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarIdShouldExistWhenSelected(request.Id);

            Car? car = await _carRepository.GetAsync(c => c.Id == request.Id);
            return car;
        }
    }
}
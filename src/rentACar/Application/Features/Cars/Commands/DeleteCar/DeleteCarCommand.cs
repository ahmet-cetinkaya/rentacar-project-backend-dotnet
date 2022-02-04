using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Cars.Commands.DeleteCar;

public class DeleteCarCommand : IRequest<Car>
{
    public int Id { get; set; }

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Car>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Car> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            Car mappedCar = _mapper.Map<Car>(request);
            Car car = await _carRepository.DeleteAsync(mappedCar);
            return car;
        }
    }
}
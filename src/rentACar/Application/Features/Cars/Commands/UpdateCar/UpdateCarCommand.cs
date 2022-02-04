using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Cars.Commands.UpdateCar;

public class UpdateCarCommand : IRequest<Car>
{
    public int Id { get; set; }
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public CarState CarState { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }

    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Car>
    {
        private ICarRepository _carRepository { get; }
        private IMapper _mapper { get; }

        public UpdateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Car> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            Car mappedCar = _mapper.Map<Car>(request);
            Car car = await _carRepository.UpdateAsync(mappedCar);
            return car;
        }
    }
}
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Cars.Commands.DeleteCar;

public class DeleteCarCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteCarResponseHandler : IRequestHandler<DeleteCarCommand>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteCarResponseHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            Car mappedCar = _mapper.Map<Car>(request);
            await _carRepository.DeleteAsync(mappedCar);
            return Unit.Value;
        }
    }
}
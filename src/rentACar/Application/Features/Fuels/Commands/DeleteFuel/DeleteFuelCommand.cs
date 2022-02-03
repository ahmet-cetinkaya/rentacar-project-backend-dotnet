using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Commands.DeleteFuel;

public class DeleteFuelCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteFuelCommandHandler : IRequestHandler<DeleteFuelCommand>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public DeleteFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            await _fuelRepository.DeleteAsync(mappedFuel);
            return Unit.Value;
        }
    }
}
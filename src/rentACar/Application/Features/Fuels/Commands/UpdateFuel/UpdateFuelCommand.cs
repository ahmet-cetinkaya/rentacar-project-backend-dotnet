using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Commands.UpdateFuel;

public class UpdateFuelCommand : IRequest<Fuel>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelCommand, Fuel>
    {
        private IFuelRepository _fuelRepository { get; }
        private IMapper _mapper { get; }

        public UpdateFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<Fuel> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            Fuel fuel = await _fuelRepository.UpdateAsync(mappedFuel);
            return fuel;
        }
    }
}
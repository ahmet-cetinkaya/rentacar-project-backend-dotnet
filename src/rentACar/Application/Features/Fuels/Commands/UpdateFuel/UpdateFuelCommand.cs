﻿using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Commands.UpdateFuel;

public class UpdateFuelCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelCommand>
    {
        private IFuelRepository _fuelRepository { get; }
        private IMapper _mapper { get; }

        public UpdateFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel fuel = _mapper.Map<Fuel>(request);
            await _fuelRepository.UpdateAsync(fuel);
            return Unit.Value;
        }
    }
}
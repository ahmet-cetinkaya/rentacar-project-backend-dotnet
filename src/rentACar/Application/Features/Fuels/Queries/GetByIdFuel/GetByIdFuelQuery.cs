using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Queries.GetByIdFuel;

public class GetByIdFuelQuery : IRequest<Fuel>
{
    public int Id { get; set; }

    public class GetByIdFuelResponseHandler : IRequestHandler<GetByIdFuelQuery, Fuel>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly FuelBusinessRules _fuelBusinessRules;

        public GetByIdFuelResponseHandler(IFuelRepository fuelRepository, FuelBusinessRules fuelBusinessRules)
        {
            _fuelRepository = fuelRepository;
            _fuelBusinessRules = fuelBusinessRules;
        }

        public async Task<Fuel> Handle(GetByIdFuelQuery request, CancellationToken cancellationToken)
        {
            await _fuelBusinessRules.FuelIdShouldExistWhenSelected(request.Id);

            Fuel? fuel = await _fuelRepository.GetAsync(f => f.Id == request.Id);
            return fuel;
        }
    }
}
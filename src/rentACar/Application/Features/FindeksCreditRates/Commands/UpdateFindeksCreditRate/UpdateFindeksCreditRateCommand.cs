using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.UpdateFindeksCreditRate;

public class UpdateFindeksCreditRateCommand : IRequest<FindeksCreditRate>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public short Score { get; set; }

    public class
        UpdateFindeksCreditRateCommandHandler : IRequestHandler<UpdateFindeksCreditRateCommand, FindeksCreditRate>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;

        public UpdateFindeksCreditRateCommandHandler(IFindeksCreditRateRepository findeksCreditRateRepository,
                                                     IMapper mapper,
                                                     FindeksCreditRateBusinessRules findeksCreditRateBusinessRules)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }

        public async Task<FindeksCreditRate> Handle(UpdateFindeksCreditRateCommand request,
                                                    CancellationToken cancellationToken)
        {
            FindeksCreditRate mappedFindeksCreditRate = _mapper.Map<FindeksCreditRate>(request);
            FindeksCreditRate updatedFindeksCreditRate =
                await _findeksCreditRateRepository.UpdateAsync(mappedFindeksCreditRate);
            return updatedFindeksCreditRate;
        }
    }
}
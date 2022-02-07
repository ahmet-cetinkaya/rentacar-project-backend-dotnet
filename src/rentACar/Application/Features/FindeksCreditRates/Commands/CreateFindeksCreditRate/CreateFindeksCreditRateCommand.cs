using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.CreateFindeksCreditRate;

public class CreateFindeksCreditRateCommand : IRequest<FindeksCreditRate>
{
    public int CustomerId { get; set; }
    public short Score { get; set; }

    public class
        CreateFindeksCreditRateCommandHandler : IRequestHandler<CreateFindeksCreditRateCommand, FindeksCreditRate>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;

        public CreateFindeksCreditRateCommandHandler(IFindeksCreditRateRepository findeksCreditRateRepository,
                                                     IMapper mapper,
                                                     FindeksCreditRateBusinessRules findeksCreditRateBusinessRules)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }

        public async Task<FindeksCreditRate> Handle(CreateFindeksCreditRateCommand request,
                                                    CancellationToken cancellationToken)
        {
            FindeksCreditRate mappedFindeksCreditRate = _mapper.Map<FindeksCreditRate>(request);
            FindeksCreditRate createdFindeksCreditRate =
                await _findeksCreditRateRepository.AddAsync(mappedFindeksCreditRate);
            return createdFindeksCreditRate;
        }
    }
}
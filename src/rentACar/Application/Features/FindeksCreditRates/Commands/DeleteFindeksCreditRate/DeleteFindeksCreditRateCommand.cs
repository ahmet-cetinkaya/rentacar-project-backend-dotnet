using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.DeleteFindeksCreditRate;

public class DeleteFindeksCreditRateCommand : IRequest<FindeksCreditRate>
{
    public int Id { get; set; }

    public class
        DeleteFindeksCreditRateCommandHandler : IRequestHandler<DeleteFindeksCreditRateCommand, FindeksCreditRate>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;

        public DeleteFindeksCreditRateCommandHandler(IFindeksCreditRateRepository findeksCreditRateRepository,
                                                     IMapper mapper,
                                                     FindeksCreditRateBusinessRules findeksCreditRateBusinessRules)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }

        public async Task<FindeksCreditRate> Handle(DeleteFindeksCreditRateCommand request,
                                                    CancellationToken cancellationToken)
        {
            await _findeksCreditRateBusinessRules.FindeksCreditRateIdShouldExistWhenSelected(request.Id);

            FindeksCreditRate mappedFindeksCreditRate = _mapper.Map<FindeksCreditRate>(request);
            FindeksCreditRate deletedFindeksCreditRate =
                await _findeksCreditRateRepository.DeleteAsync(mappedFindeksCreditRate);
            return deletedFindeksCreditRate;
        }
    }
}
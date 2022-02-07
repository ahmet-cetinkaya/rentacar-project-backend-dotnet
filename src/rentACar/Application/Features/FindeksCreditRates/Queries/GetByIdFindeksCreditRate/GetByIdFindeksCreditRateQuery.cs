using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;

public class GetByIdFindeksCreditRateQuery : IRequest<FindeksCreditRate>
{
    public int Id { get; set; }

    public class
        GetByIdFindeksCreditRateResponseHandler : IRequestHandler<GetByIdFindeksCreditRateQuery, FindeksCreditRate>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;

        public GetByIdFindeksCreditRateResponseHandler(IFindeksCreditRateRepository findeksCreditRateRepository,
                                                       FindeksCreditRateBusinessRules findeksCreditRateBusinessRules)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }


        public async Task<FindeksCreditRate> Handle(GetByIdFindeksCreditRateQuery request,
                                                    CancellationToken cancellationToken)
        {
            await _findeksCreditRateBusinessRules.FindeksCreditRateIdShouldExistWhenSelected(request.Id);

            FindeksCreditRate? findeksCreditRate = await _findeksCreditRateRepository.GetAsync(b => b.Id == request.Id);
            return findeksCreditRate;
        }
    }
}
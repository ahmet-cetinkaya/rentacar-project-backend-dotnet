using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.CorporateCustomers.Queries.GetByIdCorporateCustomer;

public class GetByIdCorporateCustomerQuery : IRequest<CorporateCustomer>
{
    public int Id { get; set; }

    public class GetByIdCorporateCustomerResponseHandler : IRequestHandler<GetByIdCorporateCustomerQuery, CorporateCustomer>
    {
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;

        public GetByIdCorporateCustomerResponseHandler(ICorporateCustomerRepository corporateCustomerRepository, CorporateCustomerBusinessRules corporateCustomerBusinessRules)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
        }


        public async Task<CorporateCustomer> Handle(GetByIdCorporateCustomerQuery request, CancellationToken cancellationToken)
        {
            await _corporateCustomerBusinessRules.CorporateCustomerIdShouldExistWhenSelected(request.Id);

            CorporateCustomer? corporateCustomer = await _corporateCustomerRepository.GetAsync(b => b.Id == request.Id);
            return corporateCustomer;
        }
    }
}

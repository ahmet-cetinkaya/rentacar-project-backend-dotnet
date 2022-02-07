using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.IndividualCustomers.Queries.GetByIdIndividualCustomer;

public class GetByIdIndividualCustomerQuery : IRequest<IndividualCustomer>
{
    public int Id { get; set; }

    public class GetByIdIndividualCustomerResponseHandler : IRequestHandler<GetByIdIndividualCustomerQuery, IndividualCustomer>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;

        public GetByIdIndividualCustomerResponseHandler(IIndividualCustomerRepository individualCustomerRepository, IndividualCustomerBusinessRules individualCustomerBusinessRules)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
        }


        public async Task<IndividualCustomer> Handle(GetByIdIndividualCustomerQuery request, CancellationToken cancellationToken)
        {
            await _individualCustomerBusinessRules.IndividualCustomerIdShouldExistWhenSelected(request.Id);

            IndividualCustomer? individualCustomer = await _individualCustomerRepository.GetAsync(b => b.Id == request.Id);
            return individualCustomer;
        }
    }
}

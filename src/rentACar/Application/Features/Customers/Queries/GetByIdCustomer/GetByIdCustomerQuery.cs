using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Customers.Queries.GetByIdCustomer;

public class GetByIdCustomerQuery : IRequest<Customer>
{
    public int Id { get; set; }

    public class GetByIdCustomerResponseHandler : IRequestHandler<GetByIdCustomerQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public GetByIdCustomerResponseHandler(ICustomerRepository customerRepository, CustomerBusinessRules customerBusinessRules)
        {
            _customerRepository = customerRepository;
            _customerBusinessRules = customerBusinessRules;
        }


        public async Task<Customer> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CustomerIdShouldExistWhenSelected(request.Id);

            Customer? customer = await _customerRepository.GetAsync(b => b.Id == request.Id);
            return customer;
        }
    }
}

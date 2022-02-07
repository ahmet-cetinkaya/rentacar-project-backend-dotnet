using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Customers.Rules;

public class CustomerBusinessRules
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerBusinessRules(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CustomerIdShouldExistWhenSelected(int id)
    {
        Customer? result = await _customerRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("Customer not exists.");
    }

    public async Task CustomerEmailCanNotBeDuplicatedWhenInserted(string email)
    {
        IPaginate<Customer> result = await _customerRepository.GetListAsync(c => c.Email == email);
        if (result.Items.Any()) throw new BusinessException("Customer email already exists.");
    }
}
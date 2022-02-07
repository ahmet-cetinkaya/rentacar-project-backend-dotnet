using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.IndividualCustomers.Rules;

public class IndividualCustomerBusinessRules
{
    private readonly IIndividualCustomerRepository _individualCustomerRepository;

    public IndividualCustomerBusinessRules(IIndividualCustomerRepository individualCustomerRepository)
    {
        _individualCustomerRepository = individualCustomerRepository;
    }

    public async Task IndividualCustomerIdShouldExistWhenSelected(int id)
    {
        IndividualCustomer? result = await _individualCustomerRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("Individual customer not exists.");
    }

    public async Task IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(string nationalIdentity)
    {
        IPaginate<IndividualCustomer> result =
            await _individualCustomerRepository.GetListAsync(c => c.NationalIdentity == nationalIdentity);
        if (result.Items.Any()) throw new BusinessException("Individual customer national identity already exists.");
    }
}
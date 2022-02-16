using Application.Features.IndividualCustomers.Dtos;
using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.IndividualCustomers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer;

public class CreateIndividualCustomerCommand : IRequest<CreatedIndividualCustomerDto>, ISecuredRequest
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }

    public string[] Roles => new[] { Admin, IndividualCustomerAdd };

    public class
        CreateIndividualCustomerCommandHandler : IRequestHandler<CreateIndividualCustomerCommand,
            CreatedIndividualCustomerDto>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;

        public CreateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualCustomerRepository,
                                                      IMapper mapper,
                                                      IndividualCustomerBusinessRules individualCustomerBusinessRules)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
        }

        public async Task<CreatedIndividualCustomerDto> Handle(CreateIndividualCustomerCommand request,
                                                               CancellationToken cancellationToken)
        {
            await _individualCustomerBusinessRules.IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(
                request.NationalIdentity);

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
            IndividualCustomer createdIndividualCustomer =
                await _individualCustomerRepository.AddAsync(mappedIndividualCustomer);
            CreatedIndividualCustomerDto createdIndividualCustomerDto =
                _mapper.Map<CreatedIndividualCustomerDto>(createdIndividualCustomer);
            return createdIndividualCustomerDto;
        }
    }
}
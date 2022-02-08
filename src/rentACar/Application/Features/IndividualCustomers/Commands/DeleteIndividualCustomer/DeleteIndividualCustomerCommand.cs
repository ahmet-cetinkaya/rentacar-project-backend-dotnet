using Application.Features.IndividualCustomers.Dtos;
using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.IndividualCustomers.Commands.DeleteIndividualCustomer;

public class DeleteIndividualCustomerCommand : IRequest<DeletedIndividualCustomerDto>
{
    public int Id { get; set; }

    public class
        DeleteIndividualCustomerCommandHandler : IRequestHandler<DeleteIndividualCustomerCommand,
            DeletedIndividualCustomerDto>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;

        public DeleteIndividualCustomerCommandHandler(IIndividualCustomerRepository individualCustomerRepository,
                                                      IMapper mapper,
                                                      IndividualCustomerBusinessRules individualCustomerBusinessRules)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
        }

        public async Task<DeletedIndividualCustomerDto> Handle(DeleteIndividualCustomerCommand request,
                                                               CancellationToken cancellationToken)
        {
            await _individualCustomerBusinessRules.IndividualCustomerIdShouldExistWhenSelected(request.Id);

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
            IndividualCustomer deletedIndividualCustomer =
                await _individualCustomerRepository.DeleteAsync(mappedIndividualCustomer);
            DeletedIndividualCustomerDto deletedIndividualCustomerDto =
                _mapper.Map<DeletedIndividualCustomerDto>(deletedIndividualCustomer);
            return deletedIndividualCustomerDto;
        }
    }
}
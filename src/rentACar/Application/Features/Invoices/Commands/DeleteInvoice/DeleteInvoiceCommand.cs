using Application.Features.Invoices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Invoices.Commands.DeleteInvoice;

public class DeleteInvoiceCommand : IRequest<Invoice>
{
    public int Id { get; set; }

    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, Invoice>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly InvoiceBusinessRules _invoiceBusinessRules;

        public DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper,
                                         InvoiceBusinessRules invoiceBusinessRules)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _invoiceBusinessRules = invoiceBusinessRules;
        }

        public async Task<Invoice> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _invoiceBusinessRules.InvoiceIdShouldExistWhenSelected(request.Id);

            Invoice mappedInvoice = _mapper.Map<Invoice>(request);
            Invoice deletedInvoice = await _invoiceRepository.DeleteAsync(mappedInvoice);
            return deletedInvoice;
        }
    }
}

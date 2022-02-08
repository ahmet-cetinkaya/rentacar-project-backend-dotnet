using Application.Features.Invoices.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Invoices.Queries.GetByIdInvoice;

public class GetByIdInvoiceQuery : IRequest<Invoice>
{
    public int Id { get; set; }

    public class GetByIdInvoiceQueryHandler : IRequestHandler<GetByIdInvoiceQuery, Invoice>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly InvoiceBusinessRules _invoiceBusinessRules;

        public GetByIdInvoiceQueryHandler(IInvoiceRepository invoiceRepository,
                                          InvoiceBusinessRules invoiceBusinessRules)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceBusinessRules = invoiceBusinessRules;
        }


        public async Task<Invoice> Handle(GetByIdInvoiceQuery request, CancellationToken cancellationToken)
        {
            await _invoiceBusinessRules.InvoiceIdShouldExistWhenSelected(request.Id);

            Invoice? invoice = await _invoiceRepository.GetAsync(b => b.Id == request.Id);
            return invoice;
        }
    }
}
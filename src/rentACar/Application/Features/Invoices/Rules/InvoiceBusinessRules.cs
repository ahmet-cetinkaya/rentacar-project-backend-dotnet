using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Invoices.Rules;

public class InvoiceBusinessRules
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceBusinessRules(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }
 
    public async Task InvoiceIdShouldExistWhenSelected(int id)
    {
        Invoice? result = await _invoiceRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("Invoice not exists.");
    }
}

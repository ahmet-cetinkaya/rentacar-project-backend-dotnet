using Domain.Entities;

namespace Application.Services.InvoiceService;

public interface IInvoiceService
{
    public Task<Invoice> CreateInvoice(Rental rental, decimal modelDailyPrice);
    public Task<Invoice> Add(Invoice invoice);
}
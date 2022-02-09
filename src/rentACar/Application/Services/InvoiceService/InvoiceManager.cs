using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.InvoiceService;

public class InvoiceManager : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceManager(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Invoice> Add(Invoice invoice)
    {
        Invoice createdInvoice = await _invoiceRepository.AddAsync(invoice);
        return createdInvoice;
    }

    public Task<Invoice> CreateInvoice(Rental rental, decimal modelDailyPrice)
    {
        short totalRentalDate = Convert.ToInt16(rental.RentEndDate.Day - rental.RentStartDate.Day > 0
                                                    ? rental.RentEndDate.Day - rental.RentStartDate.Day
                                                    : 1);
        Invoice newInvoice = new()
        {
            CustomerId = rental.CustomerId,
            No = "123123",
            RentalStartDate = rental.RentStartDate,
            RentalEndDate = rental.RentEndDate,
            TotalRentalDate = totalRentalDate,
            RentalPrice =
                Convert.ToDecimal(
                    modelDailyPrice * totalRentalDate) +
                (rental.RentStartRentalBranchId != rental.RentEndRentalBranchId ? 500 : 0)
        };
        return Task.FromResult(newInvoice);
    }
}
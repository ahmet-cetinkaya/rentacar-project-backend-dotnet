using Application.Features.Invoices.Dtos;
using Application.Features.Invoices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Invoices.Commands.UpdateInvoice;

public class UpdateInvoiceCommand : IRequest<UpdatedInvoiceDto>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string No { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public short TotalRentalDate { get; set; }
    public decimal RentalPrice { get; set; }

    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, UpdatedInvoiceDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly InvoiceBusinessRules _invoiceBusinessRules;

        public UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper,
                                           InvoiceBusinessRules invoiceBusinessRules)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _invoiceBusinessRules = invoiceBusinessRules;
        }

        public async Task<UpdatedInvoiceDto> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice mappedInvoice = _mapper.Map<Invoice>(request);
            Invoice updatedInvoice = await _invoiceRepository.UpdateAsync(mappedInvoice);
            UpdatedInvoiceDto updatedInvoiceDto = _mapper.Map<UpdatedInvoiceDto>(updatedInvoice);
            return updatedInvoiceDto;
        }
    }
}
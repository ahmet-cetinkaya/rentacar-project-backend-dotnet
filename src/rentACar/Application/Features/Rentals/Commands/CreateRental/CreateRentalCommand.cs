using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Rules;
using Application.Services.CarService;
using Application.Services.FindeksCreditRateService;
using Application.Services.InvoiceService;
using Application.Services.ModelService;
using Application.Services.POSService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Mailing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.CreateRental;

public class CreateRentalCommand : IRequest<CreatedRentalDto>, ILoggableRequest
{
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public int RentEndRentalBranchId { get; set; }

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, CreatedRentalDto>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;

        private readonly ICarService _carService;
        private readonly IFindeksCreditRateService _findeksCreditRateService;
        private readonly IInvoiceService _invoiceService;
        private readonly IModelService _modelService;
        private readonly IMailService _mailService;
        private readonly IPOSService _posService;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper,
                                          RentalBusinessRules rentalBusinessRules, ICarService carService,
                                          IFindeksCreditRateService findeksCreditRateService,
                                          IInvoiceService invoiceService, IModelService modelService,
                                          IMailService mailService, IPOSService posService)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
            _carService = carService;
            _findeksCreditRateService = findeksCreditRateService;
            _invoiceService = invoiceService;
            _modelService = modelService;
            _mailService = mailService;
            _posService = posService;
        }

        public async Task<CreatedRentalDto> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalCanNotBeCreateWhenCarIsRented(request.CarId, request.RentStartDate,
                                                                           request.RentEndDate);
            FindeksCreditRate customerFindeksCreditRate =
                await _findeksCreditRateService.GetFindeksCreditRateByCustomerId(request.CustomerId);

            Car carToBeRented = await _carService.GetById(request.CarId);

            await _rentalBusinessRules.RentalCanNotBeCreatedWhenCustomerFindeksScoreLowerThanCarMinFindeksScore(
                customerFindeksCreditRate.Score, carToBeRented.MinFindeksCreditRate);

            Model model = await _modelService.GetById(carToBeRented.ModelId);

            Rental mappedRental = _mapper.Map<Rental>(request);
            mappedRental.RentStartRentalBranchId = carToBeRented.RentalBranchId;
            mappedRental.RentStartKilometer = carToBeRented.Kilometer;

            Invoice newInvoice = await _invoiceService.CreateInvoice(mappedRental, model.DailyPrice);
            await _posService.Pay(newInvoice.No, newInvoice.RentalPrice);
            await _invoiceService.Add(newInvoice);

            Rental createdRental = await _rentalRepository.AddAsync(mappedRental);

            _mailService.SendMail(new Mail
            {
                Subject = "New Rental",
                TextBody = "A rental has been created.",
                ToEmail = "ahmetcetinkaya7@outlook.com",
                ToFullName = "Ahmet Çetinkaya"
            });

            CreatedRentalDto createdRentalDto = _mapper.Map<CreatedRentalDto>(createdRental);
            return createdRentalDto;
        }
    }
}
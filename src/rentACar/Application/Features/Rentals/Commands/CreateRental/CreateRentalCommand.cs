using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Rules;
using Application.Services.POSService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Mailing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.CreateRental;

public class CreateRentalCommand : IRequest<CreatedRentalDto>
{
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public int RentEndRentalBranchId { get; set; }

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, CreatedRentalDto>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly ICarRepository _carRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;
        private readonly IMailService _mailService;
        private readonly IPOSService _posService;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository,
                                          IFindeksCreditRateRepository findeksCreditRateRepository,
                                          ICarRepository carRepository, IMapper mapper,
                                          RentalBusinessRules rentalBusinessRules, IMailService mailService,
                                          IPOSService posService, IModelRepository modelRepository,
                                          IInvoiceRepository invoiceRepository)
        {
            _rentalRepository = rentalRepository;
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _carRepository = carRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
            _mailService = mailService;
            _posService = posService;
            _modelRepository = modelRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<CreatedRentalDto> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalCanNotBeCreateWhenCarIsRented(request.CarId, request.RentStartDate,
                                                                           request.RentEndDate);
            FindeksCreditRate? customerFindeksCreditRate =
                await _findeksCreditRateRepository.GetAsync(c => c.CustomerId == request.CustomerId);
            Car? car = await _carRepository.GetAsync(c => c.Id == request.CarId);
            await _rentalBusinessRules.RentalCanNotBeCreatedWhenCustomerFindeksScoreLowerThanCarMinFindeksScore(
                customerFindeksCreditRate!.Score, car!.MinFindeksCreditRate);

            Model? model = await _modelRepository.GetAsync(m => m.Id == car.ModelId);

            Rental mappedRental = _mapper.Map<Rental>(request);
            mappedRental.RentStartRentalBranchId = car.RentalBranchId;
            mappedRental.RentStartKilometer = car.Kilometer;

            short totalRentalDate = Convert.ToInt16(mappedRental.RentEndDate.Day - mappedRental.RentStartDate.Day > 0
                                                        ? mappedRental.RentEndDate.Day - mappedRental.RentStartDate.Day
                                                        : 1);
            Invoice invoice = new()
            {
                CustomerId = mappedRental.CustomerId,
                No = "123123",
                RentalStartDate = mappedRental.RentStartDate,
                RentalEndDate = mappedRental.RentEndDate,
                TotalRentalDate = totalRentalDate,
                RentalPrice =
                    Convert.ToDecimal(
                        model!.DailyPrice * totalRentalDate) +
                    (mappedRental.RentStartRentalBranchId != mappedRental.RentEndRentalBranchId ? 500 : 0)
            };
            await _posService.Pay(invoice.No, invoice.RentalPrice);
            await _invoiceRepository.AddAsync(invoice);

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
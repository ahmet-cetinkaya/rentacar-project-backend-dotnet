using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Mailing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.CreateRental;

public class CreateRentalCommand : IRequest<Rental>
{
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;
        private readonly IMailService _mailService;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository,
                                          IFindeksCreditRateRepository findeksCreditRateRepository,
                                          ICarRepository carRepository, IMapper mapper,
                                          RentalBusinessRules rentalBusinessRules, IMailService mailService)
        {
            _rentalRepository = rentalRepository;
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _carRepository = carRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
            _mailService = mailService;
        }

        public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalCanNotBeCreateWhenCarIsRented(request.CarId, request.RentStartDate,
                                                                           request.RentEndDate);
            FindeksCreditRate? customerFindeksCreditRate =
                await _findeksCreditRateRepository.GetAsync(c => c.CustomerId == request.CustomerId);
            Car? car = await _carRepository.GetAsync(c => c.Id == request.CarId);
            await _rentalBusinessRules.RentalCanNotBeCreatedWhenCustomerFindeksScoreLowerThanCarMinFindeksScore(
                customerFindeksCreditRate!.Score, car!.MinFindeksCreditRate);

            Rental mappedRental = _mapper.Map<Rental>(request);
            Rental createdRental = await _rentalRepository.AddAsync(mappedRental);

            _mailService.SendMail(new Mail
            {
                Subject = "New Rental",
                TextBody = "A rental has been created.",
                ToEmail = "ahmetcetinkaya7@outlook.com",
                ToFullName = "Ahmet Çetinkaya"
            });

            return createdRental;
        }
    }
}
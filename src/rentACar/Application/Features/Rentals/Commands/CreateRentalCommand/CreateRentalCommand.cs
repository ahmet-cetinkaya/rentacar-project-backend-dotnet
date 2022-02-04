using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Mailing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.CreateRentalCommand;

public class CreateRentalCommand : IRequest<Rental>
{
    public int CarId { get; set; }

    //public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;
        private readonly IMailService _mailService;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper,
                                          RentalBusinessRules rentalBusinessRules, IMailService mailService)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
            _mailService = mailService;
        }

        public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalCanNotBeCreateWhenCarIsRented(request.CarId, request.RentStartDate,
                                                                           request.RentEndDate);
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
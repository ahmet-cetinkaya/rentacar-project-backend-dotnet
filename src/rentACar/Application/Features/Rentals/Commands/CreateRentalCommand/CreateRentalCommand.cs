using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.CreateRentalCommand;

public class CreateRentalCommand : IRequest<Rental>
{
    public int CarId { get; set; }

    //public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper,
                                          RentalBusinessRules rentalBusinessRules)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
        }

        public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalCanNotBeCreateWhenCarIsRented(request.CarId, request.RentStartDate,
                                                                           request.RentEndDate);
            await _rentalBusinessRules.RentalCanNotBeCreatedWhenCarIsInMaintenance(request.CarId);

            Rental mappedRental = _mapper.Map<Rental>(request);
            Rental createdRental = await _rentalRepository.AddAsync(mappedRental);
            return createdRental;
        }
    }
}
using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.UpdateRental;

public class UpdateRentalCommand : IRequest<Rental>
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public class UpdateRentalCommandHandler : IRequestHandler<UpdateRentalCommand, Rental>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;

        public UpdateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper,
                                          RentalBusinessRules rentalBusinessRules)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
        }

        public async Task<Rental> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalCanNotBeUpdateWhenThereIsARentedCarInDate(request.Id,
                request.CarId, request.RentStartDate,
                request.RentEndDate);

            Rental mappedRental = _mapper.Map<Rental>(request);
            Rental rental = await _rentalRepository.UpdateAsync(mappedRental);
            return rental;
        }
    }
}
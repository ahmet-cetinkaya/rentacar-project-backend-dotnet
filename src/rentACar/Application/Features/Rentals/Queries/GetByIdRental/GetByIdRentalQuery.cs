using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Queries.GetByIdRental;

public class GetByIdRentalQuery : IRequest<Rental>
{
    public int Id { get; set; }

    public class GetByIdRentalResponseHandler : IRequestHandler<GetByIdRentalQuery, Rental>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly RentalBusinessRules _rentalBusinessRules;

        public GetByIdRentalResponseHandler(IRentalRepository rentalRepository, RentalBusinessRules rentalBusinessRules)
        {
            _rentalRepository = rentalRepository;
            _rentalBusinessRules = rentalBusinessRules;
        }


        public async Task<Rental> Handle(GetByIdRentalQuery request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalIdShouldExistWhenSelected(request.Id);

            Rental? rental = await _rentalRepository.GetAsync(b => b.Id == request.Id);
            return rental;
        }
    }
}
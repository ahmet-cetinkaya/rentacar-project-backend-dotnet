using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.DeleteRental;

public class DeleteRentalCommand : IRequest<Rental>

{
    public int Id { get; set; }

    public class DeleteRentalCommandHandler : IRequestHandler<DeleteRentalCommand, Rental>
    {
        private IRentalRepository _rentalRepository;
        private IMapper _mapper;

        public async Task<Rental> Handle(DeleteRentalCommand request, CancellationToken cancellationToken)
        {
            Rental mappedRental = _mapper.Map<Rental>(request);
            Rental deletedRental = await _rentalRepository.DeleteAsync(mappedRental);
            return deletedRental;
        }
    }
}
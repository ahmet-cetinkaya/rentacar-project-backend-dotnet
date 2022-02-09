using Application.Features.Rentals.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.DeleteRental;

public class DeleteRentalCommand : IRequest<DeletedRentalDto>
{
    public int Id { get; set; }

    public class DeleteRentalCommandHandler : IRequestHandler<DeleteRentalCommand, DeletedRentalDto>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public DeleteRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public async Task<DeletedRentalDto> Handle(DeleteRentalCommand request, CancellationToken cancellationToken)
        {
            Rental mappedRental = _mapper.Map<Rental>(request);
            Rental deletedRental = await _rentalRepository.DeleteAsync(mappedRental);
            DeletedRentalDto deletedRentalDto = _mapper.Map<DeletedRentalDto>(deletedRental);
            return deletedRentalDto;
        }
    }
}
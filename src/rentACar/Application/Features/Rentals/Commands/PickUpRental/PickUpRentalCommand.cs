using Application.Features.Rentals.Dtos;
using Application.Services.CarService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Commands.PickUpRental;

public class PickUpRentalCommand : IRequest<UpdatedRentalDto>
{
    public int Id { get; set; }
    public int RentEndRentalBranchId { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int RentEndKilometer { get; set; }

    public class PickUpRentalCommandHandler : IRequestHandler<PickUpRentalCommand, UpdatedRentalDto>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        private readonly ICarService _carService;

        public PickUpRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper, ICarService carService)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _carService = carService;
        }

        public async Task<UpdatedRentalDto> Handle(PickUpRentalCommand request, CancellationToken cancellationToken)
        {
            Rental rental = await _rentalRepository.GetAsync(r => r.Id == request.Id);
            rental.RentEndRentalBranchId = request.RentEndRentalBranchId;
            rental.RentEndKilometer = request.RentEndKilometer;
            rental.ReturnDate = request.ReturnDate;

            await _carService.PickUpCar(rental);

            Rental updatedRental = await _rentalRepository.UpdateAsync(rental);
            UpdatedRentalDto updatedRentalDto = _mapper.Map<UpdatedRentalDto>(updatedRental);
            return updatedRentalDto;
        }
    }
}
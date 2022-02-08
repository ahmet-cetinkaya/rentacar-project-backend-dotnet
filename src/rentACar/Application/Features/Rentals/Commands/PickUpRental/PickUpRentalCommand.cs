using Application.Features.Rentals.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
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
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public PickUpRentalCommandHandler(IRentalRepository rentalRepository, ICarRepository carRepository,
                                          IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedRentalDto> Handle(PickUpRentalCommand request, CancellationToken cancellationToken)
        {
            Rental rental = await _rentalRepository.GetAsync(r => r.Id == request.Id);
            rental.RentEndRentalBranchId = request.RentEndRentalBranchId;
            rental.RentEndKilometer = request.RentEndKilometer;
            rental.ReturnDate = request.ReturnDate;

            Car car = await _carRepository.GetAsync(c => c.Id == rental.CarId);
            car.Kilometer += Convert.ToInt32(rental!.RentEndKilometer - rental.RentStartKilometer);
            car.CarState = CarState.Available;
            await _carRepository.UpdateAsync(car);

            Rental updatedRental = await _rentalRepository.UpdateAsync(rental);
            UpdatedRentalDto updatedRentalDto = _mapper.Map<UpdatedRentalDto>(updatedRental);
            return updatedRentalDto;
        }
    }
}
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Rentals.Rules;

public class RentalBusinessRules
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ICarRepository _carRepository;


    public RentalBusinessRules(IRentalRepository rentalRepository, ICarRepository carRepository)
    {
        _rentalRepository = rentalRepository;
        _carRepository = carRepository;
    }

    public async Task RentalCanNotBeCreateWhenCarIsRented(int carId, DateTime RentStartDate, DateTime RentEndDate)
    {
        IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(
                                        r => r.CarId == carId &&
                                             r.RentEndDate >= RentStartDate &&
                                             r.RentStartDate <= RentEndDate);
        if (rentals.Items.Any()) throw new BusinessException("Rental can't be create when car is rented.");
    }

    public async Task RentalCanNotBeCreatedWhenCarIsInMaintenance(int carId)
    {
        Car? car = await _carRepository.GetAsync(c => c.Id == carId);
        if (car!.CarState == CarState.Maintenance)
            throw new BusinessException("Rental can't be created when car is in maintenance.");
    }
}
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Rentals.Rules;

public class RentalBusinessRules
{
    private readonly IRentalRepository _rentalRepository;

    public RentalBusinessRules(IRentalRepository rentalRepository, ICarRepository carRepository)
    {
        _rentalRepository = rentalRepository;
    }

    public async Task RentalCanNotBeCreateWhenCarIsRented(int carId, DateTime RentStartDate, DateTime RentEndDate)
    public async Task RentalIdShouldExistWhenSelected(int id)
    {
        Rental? result = await _rentalRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("Rental not exists.");
    }

    public async Task RentalCanNotBeCreateWhenCarIsRented(int carId, DateTime rentStartDate, DateTime rentEndDate)
    {
        IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(
                                        r => r.CarId == carId &&
                                             r.RentEndDate >= RentStartDate &&
                                             r.RentStartDate <= RentEndDate);
                                             r.RentEndDate >= rentStartDate &&
                                             r.RentStartDate <= rentEndDate);
        if (rentals.Items.Any()) throw new BusinessException("Rental can't be create when car is rented.");
    }

    public async Task RentalCanNotBeUpdateWhenThereIsARentedCarInDate(int id, int carId, DateTime rentStartDate,
                                                                      DateTime rentEndDate)
    {
        IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(
                                        r => r.Id != id && r.CarId == carId &&
                                             r.RentEndDate >= rentStartDate &&
                                             r.RentStartDate <= rentEndDate);
        if (rentals.Items.Any())
            throw new BusinessException("Rental can't be updated when there is another rented car for the date.");
    }
}
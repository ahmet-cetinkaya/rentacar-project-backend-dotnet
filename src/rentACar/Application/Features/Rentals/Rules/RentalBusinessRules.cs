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
    {
        IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(
                                        r => r.CarId == carId &&
                                             r.RentEndDate >= RentStartDate &&
                                             r.RentStartDate <= RentEndDate);
        if (rentals.Items.Any()) throw new BusinessException("Rental can't be create when car is rented.");
    }
}
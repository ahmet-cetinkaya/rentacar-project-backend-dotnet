using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Rental : Entity
{
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public virtual Car Car { get; set; }
    public virtual Customer Customer { get; set; }

    public Rental()
    {
    }

    public Rental(int id, int customerId, int carId, DateTime rentStartDate, DateTime rentEndDate, DateTime? returnDate)
    {
        Id = id;
        CustomerId = customerId;
        CarId = carId;
        RentStartDate = rentStartDate;
        RentEndDate = rentEndDate;
        ReturnDate = returnDate;
    }
}
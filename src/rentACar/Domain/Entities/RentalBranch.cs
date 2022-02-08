using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class RentalBranch : Entity
{
    public City City { get; set; }

    public virtual IEnumerable<Car> Cars { get; set; }

    public RentalBranch(int id, City city) : base(id)
    {
        City = city;
    }
}
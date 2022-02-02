using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Model : Entity
{
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public int BrandId { get; set; }
    public int TransmissionId { get; set; }
    public int FuelId { get; set; }
    public string ImageUrl { get; set; }

    public virtual Brand Brand { get; set; }
    public virtual Transmission Transmission { get; set; }
    public virtual Fuel Fuel { get; set; }
    public virtual ICollection<Car> Cars { get; set; }

    public Model()
    {
        Cars = new HashSet<Car>();
    }

    public Model(int id, string name, decimal dailyPrice, int brandId, int transmissionId, int fuelId,
                 string imageUrl) : this()
    {
        Id = id;
        Name = name;
        DailyPrice = dailyPrice;
        BrandId = brandId;
        TransmissionId = transmissionId;
        FuelId = fuelId;
        ImageUrl = imageUrl;
    }
}
using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Car : Entity
{
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public CarState CarState { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }


    public virtual Color Color { get; set; }
    public virtual Model Model { get; set; }

    public Car()
    {
    }

    public Car(int id, int colorId, int modelId, CarState carState, short modelYear, string plate,
               short minFindeksCreditRate) : base(id)
    {
        ColorId = colorId;
        ModelId = modelId;
        CarState = carState;
        ModelYear = modelYear;
        Plate = plate;
        MinFindeksCreditRate = minFindeksCreditRate;
    }
}
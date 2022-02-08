namespace Application.Features.CarDamages.Dtos;

public class CarDamageDto
{
    public int Id { get; set; }
    public string CarModelBrandName { get; set; }
    public string CarModelName { get; set; }
    public short CarModelYear { get; set; }
    public string CarPlate { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}
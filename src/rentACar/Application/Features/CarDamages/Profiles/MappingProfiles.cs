using Application.Features.CarDamages.Commands.CreateCarDamage;
using Application.Features.CarDamages.Commands.DeleteCarDamage;
using Application.Features.CarDamages.Commands.UpdateCarDamage;
using Application.Features.CarDamages.Dtos;
using Application.Features.CarDamages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.CarDamages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CarDamage, CreateCarDamageCommand>().ReverseMap();
        CreateMap<CarDamage, CreatedCarDamageDto>()
            .ForMember(c => c.CarModelBrandName, opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(c => c.CarModelName, opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(c => c.CarModelYear, opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(c => c.CarPlate, opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<CarDamage, UpdateCarDamageCommand>().ReverseMap();
        CreateMap<CarDamage, UpdatedCarDamageDto>()
            .ForMember(c => c.CarModelBrandName, opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(c => c.CarModelName, opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(c => c.CarModelYear, opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(c => c.CarPlate, opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<CarDamage, DeleteCarDamageCommand>().ReverseMap();
        CreateMap<CarDamage, DeletedCarDamageDto>()
            .ForMember(c => c.CarModelBrandName, opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(c => c.CarModelName, opt => opt.MapFrom(c => c.Car.Model.Name))
            .ReverseMap();
        CreateMap<CarDamage, CarDamageDto>()
            .ForMember(c => c.CarModelBrandName, opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(c => c.CarModelName, opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(c => c.CarModelYear, opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(c => c.CarPlate, opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<CarDamage, CarDamageListDto>()
            .ForMember(c => c.CarModelBrandName, opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(c => c.CarModelName, opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(c => c.CarModelYear, opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(c => c.CarPlate, opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<IPaginate<CarDamage>, CarDamageListModel>().ReverseMap();
    }
}
using Application.Features.Rentals.Commands.CreateRentalCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Rentals.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rental, CreateRentalCommand>().ReverseMap();
    }
}
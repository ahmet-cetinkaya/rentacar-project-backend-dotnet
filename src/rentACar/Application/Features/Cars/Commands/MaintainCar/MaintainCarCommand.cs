﻿using Application.Features.Cars.Dtos;
using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Cars.Commands.MaintainCar;

public class MaintainCarCommand : IRequest<UpdatedCarDto>
{
    public int Id { get; set; }

    public class MaintainCarCommandHandler : IRequestHandler<MaintainCarCommand, UpdatedCarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly CarBusinessRules _carBusinessRules;

        public MaintainCarCommandHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules,
                                         IMapper mapper)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdatedCarDto> Handle(MaintainCarCommand request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarIdShouldExistWhenSelected(request.Id);
            await _carBusinessRules.CarCanNotBeMaintainWhenIsRented(request.Id);

            Car? updatedCar = await _carRepository.GetAsync(c => c.Id == request.Id);
            updatedCar!.CarState = CarState.Maintenance;
            await _carRepository.UpdateAsync(updatedCar);
            UpdatedCarDto updatedCarDto = _mapper.Map<UpdatedCarDto>(updatedCar);
            return updatedCarDto;
        }
    }
}
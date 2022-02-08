﻿using Application.Features.Cars.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Cars.Commands.CreateCar;

public class CreateCarCommand : IRequest<CreatedCarDto>
{
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public CarState CarState { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CreatedCarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CreatedCarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car mappedCar = _mapper.Map<Car>(request);
            Car createdCar = await _carRepository.AddAsync(mappedCar);
            CreatedCarDto createdCarDto = _mapper.Map<CreatedCarDto>(createdCar);
            return createdCarDto;
        }
    }
}
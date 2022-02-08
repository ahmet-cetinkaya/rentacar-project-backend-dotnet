﻿using Application.Features.Colors.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.UpdateColor;

public class UpdateColorCommand : IRequest<UpdatedColorDto>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, UpdatedColorDto>
    {
        private IColorRepository _colorRepository { get; }
        private IMapper _mapper { get; }

        public UpdateColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedColorDto> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
        {
            Color mappedColor = _mapper.Map<Color>(request);
            Color updatedColor = await _colorRepository.UpdateAsync(mappedColor);
            UpdatedColorDto updatedColorDto = _mapper.Map<UpdatedColorDto>(updatedColor);
            return updatedColorDto;
        }
    }
}
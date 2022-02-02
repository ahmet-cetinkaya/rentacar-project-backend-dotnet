using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.CreateColor;

public class CreateColorCommand : IRequest<Color>
{
    public string Name { get; set; }

    public class CreateColorResponseHandler : IRequestHandler<CreateColorCommand, Color>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        private readonly ColorBusinessRules _colorBusinessRules;

        public CreateColorResponseHandler(IColorRepository colorRepository, IMapper mapper,
                                          ColorBusinessRules colorBusinessRules)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _colorBusinessRules = colorBusinessRules;
        }

        public async Task<Color> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorNameCanNotBeDuplicatedWhenInserted(request.Name);

            Color mappedColor = _mapper.Map<Color>(request);
            Color createdColor = await _colorRepository.AddAsync(mappedColor);
            return createdColor;
        }
    }
}
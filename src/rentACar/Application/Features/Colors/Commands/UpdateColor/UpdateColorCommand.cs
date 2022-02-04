using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.UpdateColor;

public class UpdateColorCommand : IRequest<Color>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, Color>
    {
        private IColorRepository _colorRepository { get; }
        private IMapper _mapper { get; }

        public UpdateColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<Color> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
        {
            Color mappedColor = _mapper.Map<Color>(request);
            Color color = await _colorRepository.UpdateAsync(mappedColor);
            return color;
        }
    }
}
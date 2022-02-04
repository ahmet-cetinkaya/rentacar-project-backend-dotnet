using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.DeleteColor;

public class DeleteColorCommand : IRequest<Color>
{
    public int Id { get; set; }

    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, Color>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public DeleteColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<Color> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            Color mappedColor = _mapper.Map<Color>(request);
            Color color = await _colorRepository.DeleteAsync(mappedColor);
            return color;
        }
    }
}
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.DeleteColor;

public class DeleteColorCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public DeleteColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            Color mappedColor = _mapper.Map<Color>(request);
            await _colorRepository.DeleteAsync(mappedColor);
            return Unit.Value;
        }
    }
}
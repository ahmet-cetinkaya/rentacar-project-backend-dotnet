using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Commands.UpdateColor;

public class UpdateColorCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand>
    {
        private IColorRepository _colorRepository { get; }
        private IMapper _mapper { get; }

        public UpdateColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
        {
            Color color = _mapper.Map<Color>(request);
            await _colorRepository.UpdateAsync(color);
            return Unit.Value;
        }
    }
}
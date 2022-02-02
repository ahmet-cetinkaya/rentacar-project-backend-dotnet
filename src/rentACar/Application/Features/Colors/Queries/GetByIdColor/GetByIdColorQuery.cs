using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Queries.GetByIdColor;

public class GetByIdColorQuery : IRequest<Color>
{
    public int Id { get; set; }

    public class GetByIdColorResponseHandler : IRequestHandler<GetByIdColorQuery, Color>
    {
        private readonly IColorRepository _colorRepository;
        private readonly ColorBusinessRules _colorBusinessRules;

        public GetByIdColorResponseHandler(IColorRepository colorRepository, ColorBusinessRules colorBusinessRules)
        {
            _colorRepository = colorRepository;
            _colorBusinessRules = colorBusinessRules;
        }


        public async Task<Color> Handle(GetByIdColorQuery request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorIdShouldExistWhenSelected(request.Id);

            Color? color = await _colorRepository.GetAsync(c => c.Id == request.Id);
            return color;
        }
    }
}
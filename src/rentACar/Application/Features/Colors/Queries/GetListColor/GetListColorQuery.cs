using Application.Features.Colors.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Queries.GetListColor;

public class GetListColorQuery : IRequest<ColorListModel>
{
    public class GetListColorResponseHandler : IRequestHandler<GetListColorQuery, ColorListModel>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public GetListColorResponseHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<ColorListModel> Handle(GetListColorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Color> colors = await _colorRepository.GetListAsync();
            ColorListModel mappedColorListModel = _mapper.Map<ColorListModel>(colors);
            return mappedColorListModel;
        }
    }
}
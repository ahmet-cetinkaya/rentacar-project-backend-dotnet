using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModel;

public class GetListModelQuery : IRequest<ModelListModel>
{
    public class GetListModelResponseHandler : IRequestHandler<GetListModelQuery, ModelListModel>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public GetListModelResponseHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await _modelRepository.GetListAsync(null, null,
                c => c.Include(c => c.Brand)
                      .Include(c => c.Fuel)
                      .Include(c => c.Transmission));
            ModelListModel mappedModelListModel = _mapper.Map<ModelListModel>(models);
            return mappedModelListModel;
        }
    }
}
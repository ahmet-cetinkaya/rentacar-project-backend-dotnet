using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModel;

public class GetListModelQuery : IRequest<ModelListModel>
{
    public PageRequest PageRequest { get; set; }

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
            IPaginate<Model> models = await _modelRepository.GetListAsync(include:
                                                                          c => c.Include(c => c.Brand)
                                                                              .Include(c => c.Fuel)
                                                                              .Include(c => c.Transmission),
                                                                          index: request.PageRequest.Page,
                                                                          size: request.PageRequest.PageSize
                                      );
            ModelListModel mappedModelListModel = _mapper.Map<ModelListModel>(models);
            return mappedModelListModel;
        }
    }
}
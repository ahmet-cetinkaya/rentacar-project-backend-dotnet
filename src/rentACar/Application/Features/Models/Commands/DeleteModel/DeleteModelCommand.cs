using Application.Features.Models.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.Models.Commands.DeleteModel;

public class DeleteModelCommand : IRequest<DeletedModelDto>, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => "models-list";

    public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand, DeletedModelDto>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public DeleteModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<DeletedModelDto> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            Model deletedModel = await _modelRepository.DeleteAsync(mappedModel);
            DeletedModelDto deletedModelDto = _mapper.Map<DeletedModelDto>(deletedModel);
            return deletedModelDto;
        }
    }
}
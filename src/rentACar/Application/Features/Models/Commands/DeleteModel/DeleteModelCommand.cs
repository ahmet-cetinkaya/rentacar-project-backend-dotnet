using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Models.Commands.DeleteModel;

public class DeleteModelCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public DeleteModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            await _modelRepository.DeleteAsync(mappedModel);
            return Unit.Value;
        }
    }
}
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Models.Commands.DeleteModel;

public class DeleteModelCommand : IRequest<Model>
{
    public int Id { get; set; }

    public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand, Model>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public DeleteModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<Model> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            Model model = await _modelRepository.DeleteAsync(mappedModel);
            return model;
        }
    }
}
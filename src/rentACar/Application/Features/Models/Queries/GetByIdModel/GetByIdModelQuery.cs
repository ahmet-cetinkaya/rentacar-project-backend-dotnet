using Application.Features.Models.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Models.Queries.GetByIdModel;

public class GetByIdModelQuery : IRequest<Model>
{
    public int Id { get; set; }

    public class GetByIdModelResponseHandler : IRequestHandler<GetByIdModelQuery, Model>
    {
        private readonly IModelRepository _modelRepository;
        private readonly ModelBusinessRules _modelBusinessRules;

        public GetByIdModelResponseHandler(IModelRepository modelRepository, ModelBusinessRules modelBusinessRules)
        {
            _modelRepository = modelRepository;
            _modelBusinessRules = modelBusinessRules;
        }


        public async Task<Model> Handle(GetByIdModelQuery request, CancellationToken cancellationToken)
        {
            await _modelBusinessRules.ModelIdShouldExistWhenSelected(request.Id);

            Model? model = await _modelRepository.GetAsync(m => m.Id == request.Id);
            return model;
        }
    }
}
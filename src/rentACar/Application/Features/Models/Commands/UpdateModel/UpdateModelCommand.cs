using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Models.Commands.UpdateModel;

public class UpdateModelCommand : IRequest<Model>
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int FuelId { get; set; }
    public int TransmissionId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }

    public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand, Model>
    {
        private IModelRepository _modelRepository { get; }
        private IMapper _mapper { get; }

        public UpdateModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<Model> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            Model model = await _modelRepository.UpdateAsync(mappedModel);
            return model;
        }
    }
}
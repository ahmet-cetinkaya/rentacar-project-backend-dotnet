using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Models.Commands.UpdateModel;

public class UpdateModelCommand : IRequest
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int FuelId { get; set; }
    public int TransmissionId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }

    public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand>
    {
        private IModelRepository _modelRepository { get; }
        private IMapper _mapper { get; }

        public UpdateModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            Model model = _mapper.Map<Model>(request);
            await _modelRepository.UpdateAsync(model);
            return Unit.Value;
        }
    }
}
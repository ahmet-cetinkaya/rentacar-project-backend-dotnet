using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.DeleteTransmission;

public class DeleteTransmissionCommand : IRequest<Transmission>
{
    public int Id { get; set; }

    public class DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, Transmission>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;

        public DeleteTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<Transmission> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission transmission = await _transmissionRepository.DeleteAsync(mappedTransmission);
            return transmission;
        }
    }
}
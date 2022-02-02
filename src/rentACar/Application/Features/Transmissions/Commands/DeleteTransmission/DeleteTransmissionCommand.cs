using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.DeleteTransmission;

public class DeleteTransmissionCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteTransmissionResponseHandler : IRequestHandler<DeleteTransmissionCommand>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;

        public DeleteTransmissionResponseHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            await _transmissionRepository.DeleteAsync(mappedTransmission);
            return Unit.Value;
        }
    }
}
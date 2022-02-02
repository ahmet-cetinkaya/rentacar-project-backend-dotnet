using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.UpdateTransmission;

public class UpdateTransmissionCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateTransmissionResponseHandler : IRequestHandler<UpdateTransmissionCommand>
    {
        private ITransmissionRepository _transmissionRepository { get; }
        private IMapper _mapper { get; }

        public UpdateTransmissionResponseHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission transmission = _mapper.Map<Transmission>(request);
            await _transmissionRepository.UpdateAsync(transmission);
            return Unit.Value;
        }
    }
}
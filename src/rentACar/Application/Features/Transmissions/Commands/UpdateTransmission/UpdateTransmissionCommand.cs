using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.UpdateTransmission;

public class UpdateTransmissionCommand : IRequest<Transmission>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateTransmissionCommandHandler : IRequestHandler<UpdateTransmissionCommand, Transmission>
    {
        private ITransmissionRepository _transmissionRepository { get; }
        private IMapper _mapper { get; }

        public UpdateTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<Transmission> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission transmission = await _transmissionRepository.UpdateAsync(mappedTransmission);
            return transmission;
        }
    }
}
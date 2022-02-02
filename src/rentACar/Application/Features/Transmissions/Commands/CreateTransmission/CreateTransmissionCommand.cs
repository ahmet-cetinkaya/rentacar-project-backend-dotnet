using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.CreateTransmission;

public class CreateTransmissionCommand : IRequest<Transmission>
{
    public string Name { get; set; }

    public class CreateTransmissionResponseHandler : IRequestHandler<CreateTransmissionCommand, Transmission>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;

        public CreateTransmissionResponseHandler(ITransmissionRepository transmissionRepository, IMapper mapper,
                                                 TransmissionBusinessRules transmissionBusinessRules)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
            _transmissionBusinessRules = transmissionBusinessRules;
        }

        public async Task<Transmission> Handle(CreateTransmissionCommand request, CancellationToken cancellationToken)
        {
            await _transmissionBusinessRules.TransmissionNameCanNotBeDuplicatedWhenInserted(request.Name);

            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission createdTransmission = await _transmissionRepository.AddAsync(mappedTransmission);
            return createdTransmission;
        }
    }
}
using Application.Features.Transmissions.Dtos;
using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Commands.CreateTransmission;

public class CreateTransmissionCommand : IRequest<CreatedTransmissionDto>
{
    public string Name { get; set; }

    public class CreateTransmissionCommandHandler : IRequestHandler<CreateTransmissionCommand, CreatedTransmissionDto>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;

        public CreateTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper,
                                                TransmissionBusinessRules transmissionBusinessRules)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
            _transmissionBusinessRules = transmissionBusinessRules;
        }

        public async Task<CreatedTransmissionDto> Handle(CreateTransmissionCommand request,
                                                         CancellationToken cancellationToken)
        {
            await _transmissionBusinessRules.TransmissionNameCanNotBeDuplicatedWhenInserted(request.Name);

            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission createdTransmission = await _transmissionRepository.AddAsync(mappedTransmission);
            CreatedTransmissionDto createdTransmissionDto = _mapper.Map<CreatedTransmissionDto>(createdTransmission);
            return createdTransmissionDto;
        }
    }
}
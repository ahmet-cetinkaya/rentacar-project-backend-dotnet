using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Queries.GetByIdTransmission;

public class GetByIdTransmissionQuery : IRequest<Transmission>
{
    public int Id { get; set; }

    public class GetByIdTransmissionResponseHandler : IRequestHandler<GetByIdTransmissionQuery, Transmission>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;

        public GetByIdTransmissionResponseHandler(ITransmissionRepository transmissionRepository,
                                                  TransmissionBusinessRules transmissionBusinessRules)
        {
            _transmissionRepository = transmissionRepository;
            _transmissionBusinessRules = transmissionBusinessRules;
        }

        public async Task<Transmission> Handle(GetByIdTransmissionQuery request, CancellationToken cancellationToken)
        {
            await _transmissionBusinessRules.TransmissionIdShouldExistWhenSelected(request.Id);

            Transmission? transmission = await _transmissionRepository.GetAsync(t => t.Id == request.Id);
            return transmission;
        }
    }
}
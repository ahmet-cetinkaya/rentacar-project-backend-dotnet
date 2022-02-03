using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand mappedBrand = _mapper.Map<Brand>(request);
            await _brandRepository.DeleteAsync(mappedBrand);
            return Unit.Value;
        }
    }
}
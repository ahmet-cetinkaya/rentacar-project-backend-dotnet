using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommand : IRequest<Brand>
{
    public int Id { get; set; }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Brand>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<Brand> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand deletedBrand = await _brandRepository.DeleteAsync(mappedBrand);
            return deletedBrand;
        }
    }
}
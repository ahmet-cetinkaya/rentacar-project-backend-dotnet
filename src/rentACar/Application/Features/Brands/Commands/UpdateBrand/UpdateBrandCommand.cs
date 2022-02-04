using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommand : IRequest<Brand>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Brand>
    {
        private IBrandRepository _brandRepository { get; }
        private IMapper _mapper { get; }

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<Brand> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand updatedBrand = await _brandRepository.UpdateAsync(mappedBrand);
            return updatedBrand;
        }
    }
}
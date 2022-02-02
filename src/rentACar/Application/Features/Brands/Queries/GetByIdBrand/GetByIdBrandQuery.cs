using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandQuery : IRequest<Brand>
{
    public int Id { get; set; }

    public class GetByIdBrandResponseHandler : IRequestHandler<GetByIdBrandQuery, Brand>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public GetByIdBrandResponseHandler(IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
        }


        public async Task<Brand> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandIdShouldExistWhenSelected(request.Id);

            Brand? brand = await _brandRepository.GetAsync(b => b.Id == request.Id);
            return brand;
        }
    }
}
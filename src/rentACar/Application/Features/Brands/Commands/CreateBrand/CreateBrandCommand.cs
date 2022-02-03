using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommand : IRequest<Brand>
{
    public string Name { get; set; }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Brand>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                                         BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
            return createdBrand;
        }
    }
}
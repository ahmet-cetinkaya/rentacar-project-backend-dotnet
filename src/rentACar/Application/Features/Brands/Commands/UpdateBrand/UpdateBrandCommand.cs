using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateBrandResponseHandler : IRequestHandler<UpdateBrandCommand>
    {
        private IBrandRepository _brandRepository { get; }
        private IMapper _mapper { get; }

        public UpdateBrandResponseHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = _mapper.Map<Brand>(request);
            await _brandRepository.UpdateAsync(brand);
            return Unit.Value; //todo: is correct?
        }
    }
}
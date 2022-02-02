using Application.Features.Cars.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Cars.Queries.GetListCar;

public class GetListCarQuery : IRequest<CarListModel>
{
    public class GetListCarResponseHandler : IRequestHandler<GetListCarQuery, CarListModel>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetListCarResponseHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CarListModel> Handle(GetListCarQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Car> cars = await _carRepository.GetListAsync(null, null,
                c => c.Include(c => c.Model)
                      .Include(c => c.Model.Brand)
                      .Include(c => c.Color));
            CarListModel mappedCarListModel = _mapper.Map<CarListModel>(cars);
            return mappedCarListModel;
        }
    }
}
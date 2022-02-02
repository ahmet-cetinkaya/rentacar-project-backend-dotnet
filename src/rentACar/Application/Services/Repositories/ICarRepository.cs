﻿using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ICarRepository : IAsyncRepository<Car>, IRepository<Car>
{
}
﻿using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Service
{
    public interface ICarService
    {
        Task<CarModel> CreateAsync(CarModel userModel);
        Task<CarModel> GetAsync(long id);
        Task<List<CarModel>> GetAllAsync();
        Task DeleteAsync(long carId);
        Task<CarModel> GetByName(string carName);
        Task<IEnumerable<CarModel>> GetByNameAsync(string carName);
        Task UpdateCar(long carId, CarModel car);
        Task<IEnumerable<CarModel>> GetAllUserCarsAsync(long userId);
        Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType);
    }
}

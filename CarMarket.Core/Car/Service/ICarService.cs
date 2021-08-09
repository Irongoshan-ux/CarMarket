﻿using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.Car.Service
{
    public interface ICarService
    {
        Task<CarModel> CreateAsync(CarModel userModel);
        Task<CarModel> GetAsync(long id);
        Task<IEnumerable<CarModel>> GetAllAsync();
        Task DeleteAsync(long carId);
        Task<CarModel> UpdateCar(long carId, CarModel car);
        Task<IEnumerable<CarModel>> GetAllUserCarsAsync(long userId);
        Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType);
    }
}

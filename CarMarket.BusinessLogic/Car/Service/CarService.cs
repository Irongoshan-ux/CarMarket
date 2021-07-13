﻿using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.Car.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.User.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<long> CreateAsync(CarModel carModel)
        {
            if (carModel is null)
            {
                throw new ArgumentNullException(nameof(carModel));
            }

            return await _carRepository.SaveAsync(carModel);
        }

        public async Task<CarModel> GetAsync(long carId)
        {
            return await _carRepository.FindByIdAsync(carId);
        }

        public async Task<List<CarModel>> GetAllAsync()
        {
            return await _carRepository.FindAllAsync();
        }

        public async Task DeleteAsync(long carId)
        {
            var carModel = await _carRepository.FindByIdAsync(carId);

            if (carModel is null)
            {
                throw new ArgumentException(nameof(carModel) + " shouldn't be null");
            }

            await _carRepository.DeleteAsync(carModel);
        }

        public CarModel GetByName(string carName)
        {
            return _carRepository.FindByName(carName);
        }

        public async Task<IEnumerable<CarModel>> GetByNameAsync(string carName)
        {
            return await _carRepository.FindByNameAsync(carName);
        }
    }
}
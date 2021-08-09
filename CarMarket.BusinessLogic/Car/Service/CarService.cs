using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Exceptions;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.Car.Service;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.Car.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserService _userService;

        public CarService(ICarRepository carRepository, IUserService userService)
        {
            _carRepository = carRepository;
            _userService = userService;
        }

        public async Task<CarModel> CreateAsync(CarModel carModel)
        {
            var owner = await _userService.GetAsync(carModel.Owner.Id);

            carModel.Owner = owner;

            return await _carRepository.AddAsync(carModel);
        }

        public async Task<CarModel> GetAsync(long carId)
        {
            return await _carRepository.FindByIdAsync(carId);
        }

        public async Task<IEnumerable<CarModel>> GetAllAsync()
        {
            return await _carRepository.FindAllAsync();
        }

        public async Task DeleteAsync(long carId)
        {
            var carModel = await _carRepository.FindByIdAsync(carId);

            if (carModel is null)
            {
                throw new CarNotFoundException($"Car with id = {carId} not found");
            }

            await _carRepository.DeleteAsync(carModel.Id);
        }

        public async Task<CarModel> GetByName(string carName)
        {
            return await _carRepository.FindOneByNameAsync(carName);
        }

        public async Task<IEnumerable<CarModel>> GetByNameAsync(string carName)
        {
            return await _carRepository.FindAllByNameAsync(carName);
        }

        public async Task<CarModel> UpdateCar(long carId, CarModel car)
        {
            var carToUpdate = await _carRepository.FindByIdAsync(carId);

            if (carToUpdate is null)
            {
                throw new CarNotFoundException($"Car with id = {carId} not found");
            }

            return await _carRepository.UpdateAsync(carId, car);
        }

        public Task<IEnumerable<CarModel>> GetAllUserCarsAsync(long userId)
        {
            return _carRepository.FindAllUserCarsAsync(userId);
        }

        public async Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType)
        {
            return await _carRepository.SearchAsync(carName, carType);
        }
    }
}

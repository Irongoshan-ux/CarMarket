using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Exceptions;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.Car.Service;
using CarMarket.Core.DataResult;
using CarMarket.Core.User.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.Car.Service
{
    public class CarServiceLogger : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserService _userService;
        private readonly ILogger<ICarService> _logger;

        public CarServiceLogger(ICarRepository carRepository, IUserService userService, ILogger<ICarService> logger)
        {
            _carRepository = carRepository;
            _userService = userService;
            _logger = logger;
        }

        public async Task<CarModel> CreateAsync(CarModel carModel)
        {
            var owner = await _userService.GetByEmailAsync(carModel.Owner.Email);

            carModel.Owner = owner;

            _logger.LogInformation($"User with id='{owner.Id}' added new car with id='{carModel.Id}'");
            return await _carRepository.AddAsync(carModel);
        }

        public async Task<CarModel> GetAsync(long carId)
        {
            _logger.LogInformation($"Find car by id={carId}");
            return await _carRepository.FindByIdAsync(carId);
        }

        public async Task<IEnumerable<CarModel>> GetAllAsync()
        {
            _logger.LogInformation($"Returned list of all cars");
            return await _carRepository.FindAllAsync();
        }

        public async Task<DataResult<CarModel>> GetByPageAsync(int skip = 0, int take = 5)
        {
            _logger.LogInformation($"Returned list of cars from id='{skip}' to id='{take}'");
            return await _carRepository.FindByPageAsync(skip, take);
        }

        public async Task DeleteAsync(long carId)
        {
            var carModel = await _carRepository.FindByIdAsync(carId);

            if (carModel is null)
            {
                throw new CarNotFoundException($"Car with id = '{carId}' not found");
            }

            _logger.LogInformation($"Car with id='{carModel.Id}' has been removed");
            await _carRepository.DeleteAsync(carModel.Id);
        }

        public async Task<CarModel> UpdateCarAsync(long carId, CarModel car)
        {
            var carToUpdate = await _carRepository.FindByIdAsync(carId);

            if (carToUpdate is null)
            {
                throw new CarNotFoundException($"Car with id = '{carId}' not found");
            }

            if (car.Owner.Email != carToUpdate.Owner.Email)
            {
                var newOwner = await _userService.GetByEmailAsync(car.Owner.Email);
                car.Owner = newOwner;
            }
            else car.Owner = null;

            _logger.LogInformation($"Car with id='{car.Id}' has been updated");
            return await _carRepository.UpdateAsync(carId, car);
        }

        public async Task<IEnumerable<CarModel>> GetAllUserCarsAsync(string userId)
        {
            _logger.LogInformation($"Returned list of cars of user id='{userId}'");
            return await _carRepository.FindAllUserCarsAsync(userId);
        }

        public async Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType)
        {
            _logger.LogInformation($"Search car with name='{carName}' and type='{carType}");

            return await _carRepository.SearchAsync(carName, carType);
        }
    }
}

using CarMarket.Core.Car.Domain;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.Car.Service;
using CarMarket.Core.User.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.Car.Service
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

        public async Task UpdateCar(long carId, CarModel car)
{
            await _carRepository.UpdateAsync(carId, car);
        }
    }
}

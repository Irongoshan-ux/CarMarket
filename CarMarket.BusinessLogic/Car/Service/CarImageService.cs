using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Repository;
using CarMarket.Core.Image.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.Car.Service
{
    public class CarImageService : ICarImageService
    {
        private readonly ICarImageRepository _carImageRepository;

        public CarImageService(ICarImageRepository carImageRepository)
        {
            _carImageRepository = carImageRepository;
        }

        public async Task<List<CarImage>> GetAllAsync(long carId)
        {
            return await _carImageRepository.FindAllAsync(carId);
        }

        public async Task<long> UploadAsync(CarImage carImage)
        {
            return await _carImageRepository.SaveAsync(carImage);
        }
    }
}

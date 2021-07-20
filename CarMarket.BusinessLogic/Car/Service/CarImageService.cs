using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Repository;
using CarMarket.Core.Image.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.Car.Service
{
    public class CarImageService : IImageService
    {
        private readonly IImageRepository _carImageRepository;

        public CarImageService(IImageRepository carImageRepository)
        {
            _carImageRepository = carImageRepository;
        }

        public async Task<List<ImageModel>> GetAllAsync(long carId)
        {
            return await _carImageRepository.FindAllAsync(carId);
        }

        public async Task<long> UploadAsync(ImageModel carImage)
        {
            return await _carImageRepository.SaveAsync(carImage);
        }
    }
}

using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Repository;
using CarMarket.Core.Image.Service;
using System;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.Car.Service
{
    class CarImageService : ICarImageService
    {
        private readonly ICarImageRepository _carImageRepository;

        public CarImageService(ICarImageRepository carImageRepository)
        {
            _carImageRepository = carImageRepository;
        }

        public async Task<long> UploadAsync(CarImage carImage)
        {
            return await _carImageRepository.SaveAsync(carImage);
        }
    }
}

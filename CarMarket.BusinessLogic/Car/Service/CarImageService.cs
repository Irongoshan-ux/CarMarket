using CarMarket.Core.Image.Domain;
using CarMarket.Core.Image.Service;
using System;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.Car.Service
{
    class CarImageService : ICarImageService
    {
        public Task<long> UploadAsync(CarImage carImage)
        {
            throw new NotImplementedException();
        }
    }
}

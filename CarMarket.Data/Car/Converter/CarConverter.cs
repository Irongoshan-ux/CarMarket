using CarMarket.Core.Car.Domain;
using CarMarket.Core.Image.Domain;
using CarMarket.Data.Car.Domain;
using System.Collections.Generic;

namespace CarMarket.Data.Car.Converter
{
    public class CarConverter
    {
        public CarEntity ToEntity(CarModel carModel)
        {
            return new CarEntity(carModel.Id, carModel.Name, carModel.CarType, carModel.CarImages, carModel.Description, carModel.Price);
        }

        public CarModel ToModel(CarEntity carEntity)
        {
            return new CarModel(carEntity.Id, carEntity.Name, carEntity.CarType, carEntity.CarImages, carEntity.Description, carEntity.Price);
        }
    }
}

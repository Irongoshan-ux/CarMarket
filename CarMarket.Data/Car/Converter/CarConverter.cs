using CarMarket.Core.Car.Domain;
using CarMarket.Data.Car.Domain;

namespace CarMarket.Data.Car.Converter
{
    public class CarConverter
    {
        public CarEntity ToEntity(CarModel carModel)
        {
            return new CarEntity(carModel.Id, carModel.Name, carModel.CarType, carModel.Description, carModel.Price);
        }

        public CarModel ToModel(CarEntity carEntity)
        {
            return new CarModel(carEntity.Id, carEntity.Name, carEntity.CarType, carEntity.Description, carEntity.Price);
        }
    }
}

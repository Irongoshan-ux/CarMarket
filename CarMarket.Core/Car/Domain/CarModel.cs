using CarMarket.Core.Image.Domain;
using System.Collections.Generic;

namespace CarMarket.Core.Car.Domain
{
    public class CarModel
    {
        public CarModel()
        {

        }

        public CarModel(long id, string name, CarType carType, ICollection<CarImage> carImages, string description, int price)
        {
            Id = id;
            Name = name;
            CarType = carType;
            CarImages = carImages;
            Description = description;
            Price = price;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public CarType CarType { get; set; }
        public ICollection<CarImage> CarImages { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
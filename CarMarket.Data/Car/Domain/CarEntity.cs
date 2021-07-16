using CarMarket.Core.Car.Domain;
using System.Collections.Generic;

namespace CarMarket.Data.Car.Domain
{
    public class CarEntity
    {
        public CarEntity()
        {
        }

        public CarEntity(long id, string name, CarType carType, ICollection<CarImage> carImages, string description, int price)
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
        public long? CarImageId { get; set; }
        public ICollection<CarImage> CarImages { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}

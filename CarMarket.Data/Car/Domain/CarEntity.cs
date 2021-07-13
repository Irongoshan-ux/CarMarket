using CarMarket.Core.Car.Domain;

namespace CarMarket.Data.Car.Domain
{
    public class CarEntity
    {
        public CarEntity()
        {
        }

        public CarEntity(long id, string name, Type carType, string description, int price)
        {
            Id = id;
            Name = name;
            CarType = carType;
            Description = description;
            Price = price;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public Type CarType { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}

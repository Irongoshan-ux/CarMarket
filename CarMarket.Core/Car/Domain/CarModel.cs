namespace CarMarket.Core.Car.Domain
{
    public class CarModel
    {
        public CarModel()
        {

        }

        public CarModel(long id, string name, Type carType, string description, int price)
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
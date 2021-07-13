namespace CarMarket.Core.Car.Domain
{
    public class CarModel
    {
        public CarModel()
        {

        }

        public CarModel(int id, string name, string carType, string description, int price)
        {
            Id = id;
            Name = name;
            CarType = carType;
            Description = description;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CarType { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
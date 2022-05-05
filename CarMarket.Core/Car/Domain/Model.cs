namespace CarMarket.Core.Car.Domain
{
    public class Model
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CarType Type { get; set; }
        public Brand Brand { get; set; }
    }
}
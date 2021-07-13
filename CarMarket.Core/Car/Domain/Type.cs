namespace CarMarket.Core.Car.Domain
{
    public enum CarType
    {
        Sedan,
        Coupe,
        SportCar,
        StationWagon,
        Hatchback,
        Convertible,
        SportUtilityVehicle,
        Minivan,
        Pickup
    }

    public class Type
    {
        public int Id { get; set; }
        public CarType CarTypeName { get; set; }
    }
}

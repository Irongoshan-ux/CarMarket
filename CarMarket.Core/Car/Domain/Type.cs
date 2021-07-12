namespace CarMarket.Core.Car.Domain
{
    public class Type
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

        public int Id { get; set; }
        public CarType CarTypeName { get; set; }
    }
}

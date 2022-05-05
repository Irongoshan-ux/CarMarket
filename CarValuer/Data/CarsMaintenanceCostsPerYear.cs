namespace CarValuer.Data
{
    public class CarsMaintenanceCostsPerYear
    {
        public long Id { get; set; }
        public Car Car { get; set; }
        public int Cost { get; set; }
    }
}

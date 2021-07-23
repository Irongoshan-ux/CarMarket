using CarMarket.Core.Car.Domain;
using CarMarket.Core.Image.Domain;
using System.Collections.Generic;

namespace CarMarket.Data.Car.Domain
{
    public class CarEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CarType CarType { get; set; }
        public ICollection<ImageModel> CarImages { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}

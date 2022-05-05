using CarMarket.Core.Car.Domain;
using System.ComponentModel.DataAnnotations;

namespace CarValuer.Data
{
    public class Car
    {
        public long Id { get; set; }

        [Required]
        public Model Model { get; set; }

        public int Price { get; set; }

        public DateTime Year { get; set; }
    }
}
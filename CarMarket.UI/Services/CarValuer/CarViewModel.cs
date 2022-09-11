using CarMarket.Core.Car.Domain;
using System.ComponentModel.DataAnnotations;
using System;

namespace CarMarket.UI.Services.CarValuer
{
    public class CarViewModel
    {
        public long Id { get; set; }

        [Required]
        public Model Model { get; set; }

        public int Price { get; set; }

        public DateTime Year { get; set; }
    }
}
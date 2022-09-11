using CarMarket.Core.Car.Domain;
using CarMarket.Core.Image.Domain;
using CarMarket.Data.User.Domain;
using System;
using System.Collections.Generic;

namespace CarMarket.Data.Car.Domain
{
    public class CarEntity
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public ICollection<ImageModel> CarImages { get; set; }
        public int Price { get; set; }
        public UserEntity Owner { get; set; }
        public Model Model { get; set; }
        public DateTime Year { get; set; }
    }
}

﻿using CarMarket.Core.Image.Domain;
using CarMarket.Core.User.Domain;
using System.Collections.Generic;

namespace CarMarket.Core.Car.Domain
{
    public class CarModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CarType CarType { get; set; }
        public ICollection<ImageModel> CarImages { get; set; }
        public UserModel Owner { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
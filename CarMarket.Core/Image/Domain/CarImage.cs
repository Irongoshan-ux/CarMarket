﻿namespace CarMarket.Core.Image.Domain
{
    public class CarImage
    {
        public long Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}

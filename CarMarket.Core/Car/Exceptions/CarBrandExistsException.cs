using System;

namespace CarMarket.Core.Car.Exceptions
{
    public class CarBrandExistsException : Exception
    {
        public CarBrandExistsException()
        {
        }

        public CarBrandExistsException(string message) : base(message)
        {
        }
    }
}
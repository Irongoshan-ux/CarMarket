using System;

namespace CarMarket.Core.Car.Exceptions
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException() : base()
        {
        }

        public CarNotFoundException(string message)
            : base(message)
        {
        }
    }
}

using System;
using System.Runtime.Serialization;

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

        public CarBrandExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarBrandExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
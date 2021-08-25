using System;

namespace CarMarket.Core.Image.Exceptions
{
    public class FileNotSupportedException : Exception
    {
        public FileNotSupportedException()
        {
            
        }

        public FileNotSupportedException(string message) 
            : base(message)
        {
        }
    }
}

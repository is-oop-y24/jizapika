using System;

namespace Shops.Exceptions
{
    public class ShoppingSystemException : Exception
    {
        public ShoppingSystemException()
        {
        }

        public ShoppingSystemException(string message)
            : base(message)
        {
        }

        public ShoppingSystemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
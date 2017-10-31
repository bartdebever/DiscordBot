using System;

namespace DataLibrary.Shop.Exceptions
{
    public class CoinBuyException : Exception
    {
        public CoinBuyException()
        {
        }

        public CoinBuyException(string message) : base(message)
        {
        }

        public CoinBuyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

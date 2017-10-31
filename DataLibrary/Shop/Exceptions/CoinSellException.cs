using System;

namespace DataLibrary.Shop.Exceptions
{
    public class CoinSellException : Exception
    {
        public CoinSellException()
        {
        }

        public CoinSellException(string message) : base(message)
        {
        }

        public CoinSellException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

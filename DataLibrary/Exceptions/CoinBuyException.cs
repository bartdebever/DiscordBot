using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Exceptions
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

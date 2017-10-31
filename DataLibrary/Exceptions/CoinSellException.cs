using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Exceptions
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

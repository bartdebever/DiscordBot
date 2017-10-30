using System;

namespace Riot.Exceptions
{
    class RiotNETException : Exception
    {
        public RiotNETException()
        {
        }

        public RiotNETException(string message)
            : base(message)
        {
        }

        public RiotNETException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

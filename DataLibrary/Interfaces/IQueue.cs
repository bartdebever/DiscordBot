using System.Collections.Generic;

namespace DataLibrary.Interfaces
{
    public interface IQueue
    {
        IUser Owner { get; set; }
        List<IUser> Players { get; set; }
        int PlayerCount { get; set; }
    }
}
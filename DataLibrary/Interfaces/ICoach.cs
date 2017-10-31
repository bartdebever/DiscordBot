using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Interfaces
{
    /// <summary>
    /// A coach is someone to teach another user about the ins and out of a game.
    /// This might involve payment.
    /// </summary>
    public interface ICoach : IVoteable
    {
        /// <summary>
        /// The user which is giving the coaching service.
        /// Can be NULL
        /// </summary>
        IUser User { get; set; }
        /// <summary>
        /// The name of the coach.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The game which he specifies in.
        /// </summary>
        IGame Game { get; set; }
    }
}

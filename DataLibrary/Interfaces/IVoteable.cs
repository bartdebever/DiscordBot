using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Interfaces
{
    /// <summary>
    /// This interface is made to easily make things voteable for.
    /// This means servers but also events or tournaments.
    /// </summary>
    public interface IVoteable
    {
        /// <summary>
        /// The amount of votes stored as an Integer
        /// </summary>
        int Votes { get; set; }
        /// <summary>
        /// The users that have already voted for the object
        /// </summary>
        List<IUser> Voters { get; set; }
        /// <summary>
        /// Adds a surtain amount of votes to the counter
        /// </summary>
        /// <param name="user">The user who is voting in question</param>
        /// <param name="weight">The amount the vote weights</param>
        void Vote(IUser user, int weight);
        /// <summary>
        /// Removes the vote from the server.
        /// </summary>
        /// <param name="user">The user who is removing the vote</param>
        /// <param name="weight">The weight of the vote</param>
        void UnVote(IUser user, int weight);
    }
}

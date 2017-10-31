using System.Collections.Generic;
using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Matchmaking.Interfaces
{
    /// <summary>
    /// The Party class is used for the matchmaking system.
    /// A party must always contain one player or else it should be deleted.
    /// Parties will only be stored in RAM and not in the actual database.s
    /// </summary>
    public interface IParty
    {
        /// <summary>
        /// The user who created the party.
        /// </summary>
        IUser Owner { get; set; }
        /// <summary>
        /// A list of users in the party excluding the owner.
        /// </summary>
        List<IUser> Players { get; set; }
        /// <summary>
        /// The maximal amount of players allowed in this party.
        /// </summary>
        int MaxPlayerCount { get; set; }
    }
}
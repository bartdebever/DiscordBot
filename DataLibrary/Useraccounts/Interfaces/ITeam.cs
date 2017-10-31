using System.Collections.Generic;

namespace DataLibrary.Useraccounts.Interfaces
{
    /// <summary>
    /// A team a user can be part of.
    /// A user can only be part of one team at a time.
    /// </summary>
    public interface ITeam
    {
        /// <summary>
        /// The Id that is assigned to the team.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// This is the name the owner has given the team.
        /// This should be without any discriminating words in it.
        /// Swearing is okay, discrimination isn't.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// A short string to identify the team by.
        /// Must be Unique
        /// As before, swearing is okay, discrimination isn't.
        /// </summary>
        string Tag { get; set; }
        /// <summary>
        /// The user who has created the team.
        /// </summary>
        IUser Owner { get; set; }
        /// <summary>
        /// The users within the team.
        /// </summary>
        List<IUser> Users { get; set; }
        /// <summary>
        /// A list of users wanting to join the team but awaiting acception
        /// </summary>
        List<IUser> Requests { get; set; }
        /// <summary>
        /// Adds the to the request list.
        /// Checks if the user is already in a request list or in a team.
        /// </summary>
        /// <param name="user">The user wanting to join the team</param>
        void Join(IUser user);
        /// <summary>
        /// Makes the user leave the team.
        /// This function will also be used when kicking people.
        /// </summary>
        /// <param name="user">the user leaving or being kicked</param>
        void Leave(IUser user);
        /// <summary>
        /// Accepts the user in the Requests list.
        /// Only the owner can do this TODO Inplement moderators
        /// </summary>
        /// <param name="user">The user wanting to join the team</param>
        void Accept(IUser user);
    }
}

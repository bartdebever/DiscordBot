using System.Collections.Generic;
using DataLibrary.Interfaces;

namespace DataLibrary.Useraccounts.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Id is used so multiple users can have multiple names.
        /// An Id should be a random (hex) unique number so people can not look at other users if they know their id.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Name will not have to be unique and can be anything.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Get's a list of summoner the user owns
        /// </summary>
        List<ISummoner> Summoners { get; set; }
        /// <summary>
        /// Adds a summoner to the list
        /// </summary>
        /// <param name="summoner">A verified summoner that the user owns.</param>
        void AddSummoner(ISummoner summoner);
    }
}
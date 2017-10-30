using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Interfaces
{
    /// <summary>
    /// Tournaments can be added to the system and advertised through listing.
    /// </summary>
    public interface ITournament
    {
        /// <summary>
        /// The internal Id for this tournanament
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// The name of the tournament series
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Which edition of the tournament this is, this can be left emtpy or null.
        /// </summary>
        string Edition { get; set; }
        /// <summary>
        /// What games are being played at the tournament.
        /// </summary>
        List<IGame> Games { get; set; }
        /// <summary>
        /// What servers are hosting the tournament.
        /// </summary>
        List<IServer> Servers { get; set; }
        /// <summary>
        /// The TimeSpan which the tournament is held in.
        /// </summary>
        TimeSpan TimeSpan { get; set; }
        /// <summary>
        /// How users can sign up to participate in the tournament.
        /// </summary>
        string SignUpMethod { get; set; }
        /// <summary>
        /// The description of the tournament being held.
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// A list of attendees can be NULL
        /// </summary>
        List<IUser> Attendees { get; set; }
        /// <summary>
        /// Links going towards streams and social media of the event giver.
        /// </summary>
        List<string> Link { get; set; }
        /// <summary>
        /// Method used to add a link.
        /// Denies discord invite links to be added.
        /// Throws an exception when the link is invalid
        /// </summary>
        /// <param name="link"></param>
        void AddLink(string link);
        /// <summary>
        /// Method to remove a link.
        /// Throws and exception when link isn't present
        /// </summary>
        /// <param name="link"></param>
        void RemoveLink(string link);
    }
}

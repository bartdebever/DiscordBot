using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Interfaces;

namespace DataLibrary.Discord
{
    public class DiscordServer : IServer, IVoteable
    {
        public string Name { get; set; }
        public string JoinMethod { get; set; }
        public string Description { get; set; }
        public IUser Owner { get; set; }
        /// <summary>
        /// Id used by the DiscordAPI to identify servers.
        /// </summary>
        public long ServerId { get; set; }

        public int Votes { get; set; }
        public List<IUser> Voters { get; set; }
        public void Vote(IUser user, int weight)
        {
            if (!Voters.Contains(user))
            {
                Votes += (1 * weight);
                Voters.Add(user);
            }
        }

        public void UnVote(IUser user, int weight)
        {
            if (Voters.Contains(user))
            {
                Votes -= (1 * weight);
                Voters.Remove(user);
            }
        }
    }
}

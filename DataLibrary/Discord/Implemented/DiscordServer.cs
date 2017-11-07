using System;
using System.Collections.Generic;
using DataLibrary.Interfaces;
using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Discord.Implemented
{
    public class DiscordServer : IServer, IVoteable
    {
        public int Id { get; set; }
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
        public DateTime JoinDateTime { get; set; }
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

        public DiscordServer(int id, string name, IUser owner, long serverid, DateTime joinDateTime)
        {
            Id = id;
            Voters = new List<IUser>();
            Votes = 0;
            Name = name;
            Owner = owner;
            ServerId = serverid;
            this.JoinDateTime = joinDateTime;
        }
        public DiscordServer() { }
    }
}

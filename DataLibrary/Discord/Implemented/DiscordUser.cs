using System.Collections.Generic;
using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Discord.Implemented
{
    public class DiscordUser : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ISummoner> Summoners { get; set; }
        public void AddSummoner(ISummoner summoner)
        {
            if (!Summoners.Contains(summoner))
            {
                Summoners.Add(summoner);
            }
        }

        /// <summary>
        /// The Id used by Discord to identify the user.
        /// </summary>
        public long Discordid { get; set; }
    }
}

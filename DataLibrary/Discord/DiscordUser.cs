using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Interfaces;

namespace DataLibrary
{
    public class DiscordUser : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// The Id used by Discord to identify the user.
        /// </summary>
        public long Discordid { get; set; }
    }
}

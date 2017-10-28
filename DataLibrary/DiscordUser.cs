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
        public long Discordid { get; set; }
    }
}

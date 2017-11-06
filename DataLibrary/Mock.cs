using System.Data.Entity;
using DataLibrary.Discord.Implemented;

namespace DataLibrary
{
    public class Mock : DbContext
    { 
        /// <summary>
        /// Used to test the EntityFramework and how our system functions.
        /// </summary>
        public DbSet<DiscordUser> Users { get; set; }
    }
}

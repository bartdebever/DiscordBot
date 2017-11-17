using System.Data.Entity;
using DataLibrary.Discord.Implemented;
using DataLibrary.Useraccounts.Implementation;

namespace DataLibrary
{
    public class Mock : DbContext
    { 
        /// <summary>
        /// Used to test the EntityFramework and how our system functions.
        /// </summary>
        public DbSet<DiscordUser> Users { get; set; }
        public DbSet<DiscordServer> Servers { get; set; }
        public DbSet<APISummoner> Summoners { get; set; }

        /// <summary>
        /// https://stackoverflow.com/questions/3600175/the-model-backing-the-database-context-has-changed-since-the-database-was-crea
        /// </summary>
        /// <param name="modelBuilder"></param>
        public Mock()
        {
            //Database.SetInitializer<Mock>(null);
        }
    }
}

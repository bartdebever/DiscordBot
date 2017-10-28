using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class Mock : DbContext
    {
        public DbSet<DiscordUser> Users { get; set; }
    }
}

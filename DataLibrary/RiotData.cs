using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class RiotData : DbContext
    {
        public DbSet<Champion> Champions { get; set; }
        public RiotData() : base("MyConnectionStringName")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RiotData, Configuration2>());
        }
    }
    internal sealed class Configuration2 : DbMigrationsConfiguration<RiotData>
    {
        public Configuration2()
        {
            AutomaticMigrationsEnabled = true;
        }

    }
}

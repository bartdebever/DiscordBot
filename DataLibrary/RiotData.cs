using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Riot.Item;

namespace DataLibrary
{
    public class RiotData : DbContext
    {
        public DbSet<Champion> Champions { get; set; }
        public DbSet<ItemDto> Items { get; set; }
        public DbSet<ChampionSpellDto> Spells { get; set; }
        public DbSet<PassiveDto> Passives { get; set; }
        public DbSet<StatsDto> Stats { get; set; }
        public DbSet<SkinDto> Skins { get; set; }
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

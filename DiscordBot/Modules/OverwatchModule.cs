using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;
using OverwatchHandler;

namespace DiscordBot.Modules
{
    [Group("Overwatch")]
    public class OverwatchModule : ModuleBase
    {
        [Command("Hero")]
        public async Task GetHero([Remainder] string name)
        {
            var hero = RequestHandler.GetHeroByName(name);
            string subroles = "";
            hero.sub_roles.ForEach(x => subroles+= x.name + ", ");
            if (subroles.Length > 0)
            subroles = subroles.Remove(subroles.Length - 2, 2);
            var builder = Builders.BaseBuilder(hero.name, "", Color.LightOrange, null, "");
            builder.AddField("Information", $"**Name: **{hero.name}\n" +
                                            $"**Main Role: **{hero.role.name}\n" +
                                            $"**Sub Roles: **{subroles}");
            string lore = "";
            if (!string.IsNullOrEmpty(hero.real_name)) lore += $"**Real Name: **{hero.real_name}\n";
            if (!string.IsNullOrEmpty(hero.affiliation)) lore += $"**Affiliation: **{hero.affiliation}\n";
            if (!string.IsNullOrEmpty(hero.base_of_operations)) lore += $"**Base of Operations: **{hero.base_of_operations}\n";
            if (hero.age != null) lore += $"**Age: **{hero.age}\n";
            if (hero.height != null) lore += $"**Height: **{hero.height}\n";
            builder.AddField("Lore", lore);
            builder.AddField("Stats",
                $"**Health: **{hero.health}\n" +
                $"**Armor: **{hero.armor}\n" +
                $"**Shield: **{hero.shield}");
            string abilityInfo = "";
            foreach (var overwatchAbility in hero.abilities)
            {
                if (overwatchAbility.is_ultimate)
                {
                    abilityInfo += "***Ultimate***\n";
                }
                abilityInfo += $"**{overwatchAbility.name}**: {overwatchAbility.description}\n";
            }
            builder.AddField("Abilities", abilityInfo);
            await ReplyAsync("", embed: builder.Build());
        }
    }
}

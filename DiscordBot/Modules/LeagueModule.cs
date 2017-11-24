using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChampionGGHandler;
using DataLibrary;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;

namespace DiscordBot.Modules
{
    [Group("lol")]
    public class LeagueModule : ModuleBase
    {
        [Command("iteminit")]
        public async Task Init()
        {
            Temp.GetItems();
            await ReplyAsync("Done!");
        }

        [Command("item")]
        public async Task GetItem([Remainder] string name)
        {
            var database = new RiotData();
            var item = database.Items.FirstOrDefault(x => x.name.ToLower().Contains(name.ToLower()));
            var builder = Builders.BaseBuilder("", "", Color.DarkBlue, new EmbedAuthorBuilder().WithName(item.name),
                $"http://ddragon.leagueoflegends.com/cdn/6.24.1/img/item/{item.ItemId}.png");
            builder.AddField("Description", Regex.Replace(item.description.Replace("<br>", "\n"), "<.*?>", String.Empty));
            builder.AddField("Effect", item.plaintext);
            builder.AddField("Cost", $"**Total: **{item.gold.total}\n" +
                                     $"**Base: **{item.gold.Base}");
            if (!string.IsNullOrEmpty(item.requiredChampion))
            {
                builder.AddField("Required Champion", item.requiredChampion);
            }
            await ReplyAsync("", embed: builder.Build());

        }

        [Command("champinit")]
        public async Task ChampInit()
        {
            Temp.GetChammpions();
            await ReplyAsync("Done!");
        }

        [Command("champion")]
        public async Task Champion([Remainder] string name)
        {
            var database = new RiotData();
            List<DataLibrary.Champion> champions = database.Champions.Include("spells").Include("skins").Include("passive").ToList();
            var champ = champions.FirstOrDefault(x => x.name.ToLower().Contains(name.ToLower()));
            var builder = Builders.BaseBuilder(champ.name, champ.title, Color.DarkBlue, null, $"http://ddragon.leagueoflegends.com/cdn/6.24.1/img/champion/{champ.key}.png");
            //builder.AddField("Stats", $"**Dificulty: **{champ.stats.difficulty}\n" +
            //                          $"**Attack: **{champ.stats.attack}\n" +
            //                          $"**Magic: **{champ.stats.magic}\n" +
            //                          $"**Defense: **{champ.stats.defense}");
            var keys = new List<string>() {"Q", "W", "E", "R"};
            string abilities = "";
            for(int i = 0; i< 4; i++)
            {
                abilities += $"**{keys[i]} {champ.spells[i].name}:**  {champ.spells[i].sanitizedDescription}\n";
            }
            builder.AddField("Passive", $"**{champ.passive.name}: **{champ.passive.description}");
            builder.AddField("Abilities", abilities);
            string skins = "";
            champ.skins.ForEach(x=> skins += x.name + "\n");
            builder.AddField("Skins", skins);
            await ReplyAsync("", embed: builder.Build());
        }
    }
}

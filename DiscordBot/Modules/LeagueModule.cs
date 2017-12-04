using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChampionGGHandler;
using DataLibrary;
using DataLibrary.Static_Data;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.EmbedBuilder;
using RiotWrapper;
using RiotWrapper.DataTypes;

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
            var item = database.Items.FirstOrDefault(x => x.name.ToLower().Equals(name.ToLower()));
            if (item == null)
            {
                var items = database.Items.Where(x => x.name.ToLower().Contains(name.ToLower())).ToList();
                var builder = Builders.BaseBuilder("Multiple items found", "", Color.Red, null, "");
                string itemsstring = "";
                items.ForEach(x=> itemsstring += x.name +"\n");
                builder.AddField("Items", itemsstring);
                await ReplyAsync("", embed: builder.Build());
            }
            else
            { 
                var builder = Builders.BaseBuilder("", "", Color.DarkBlue, new EmbedAuthorBuilder().WithName(item.name),
                    $"http://ddragon.leagueoflegends.com/cdn/6.24.1/img/item/{item.ItemId}.png");
                builder.AddField("Effect", item.plaintext);
                builder.AddField("Tooltip",
                    Regex.Replace(item.description.Replace("<br>", "\n"), "<.*?>", String.Empty));
                builder.AddField("Cost", $"**Total: **{item.gold.total}\n" +
                                         $"**Base: **{item.gold.Base}");
                if (!string.IsNullOrEmpty(item.requiredChampion))
                {
                    builder.AddField("Required Champion", item.requiredChampion);
                }
                if (item.consumed)
                {
                    builder.AddField("Consumable", $"This item is a consumable and a play can have {item.stacks} of this item at a time.");
                }
                await ReplyAsync("", embed: builder.Build());

            }


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
            List<DataLibrary.Champion> champions = database.Champions.Include("spells").Include("skins").Include("passive").Include("stats").ToList();
            DataLibrary.Champion champ = null;
            champ = champions.FirstOrDefault(x => x.name.ToLower().Equals(name.ToLower()));
            if(champ == null)
            champ = champions.FirstOrDefault(x => x.name.ToLower().Contains(name.ToLower()));
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

        [Command("skin")]
        public async Task Skin([Remainder] string name)
        {
            var database= new RiotData();
            var skins = database.Skins;
            var skinlist = skins.Where(x => x.name.ToLower().Contains(name.ToLower())).ToList();
            if (skinlist.Count() > 1)
            {
                var builder = Builders.BaseBuilder("Error: Multiple found", "", Color.Red, null, "");
                string doubes = "";
                skinlist.ForEach(x=> doubes += x.name + "\n");
                builder.AddField("Skins", doubes);
                await ReplyAsync("", embed: builder.Build());
            }
            else
            {
                var skin = skinlist[0];
                Champion champ = null;
                foreach (var databaseChampion in database.Champions.Include("skins"))
                {
                    foreach (var databaseChampionSkin in databaseChampion.skins)
                    {
                        if (databaseChampionSkin.id == skin.id)
                        {
                            champ = databaseChampion;
                        }
                    }
                }
                var builder = Builders.BaseBuilder("", "", Color.DarkBlue, null, "");
                builder.WithImageUrl($"http://ddragon.leagueoflegends.com/cdn/img/champion/splash/{champ.key}_{skin.num}.jpg");
                await ReplyAsync("", embed: builder.Build());
            }
        }

        [Command("skill")]
        public async Task Skill([Summary("Champion name")]string champion, [Summary("Ability used")] string ability)
        {
            var database = new RiotData();
            List<DataLibrary.Champion> champions = database.Champions.Include("spells").Include("skins").Include("passive").Include("stats").Include("spells.vars").Include("spells.vars.Coeff").ToList();
            DataLibrary.Champion champ = null;
            champ = champions.FirstOrDefault(x => x.name.ToLower().Equals(champion.ToLower()));
            if (champ == null)
                champ = champions.FirstOrDefault(x => x.name.ToLower().Contains(champion.ToLower()));
            var keys = new List<string>() { "Q", "W", "E", "R" };
            int index = keys.IndexOf(ability.ToUpper());
            var spell = champ.spells[index];
            var builder = Builders.BaseBuilder(spell.name, "", Color.DarkBlue,
                new EmbedAuthorBuilder().WithName(champ.name + " " + ability), $"http://ddragon.leagueoflegends.com/cdn/6.24.1/img/spell/{spell.key}.png ");
            builder.AddField("Description", spell.sanitizedDescription);
            string tooltip = spell.sanitizedTooltip;

            foreach (var spellVar in spell.vars)
            {
                tooltip = tooltip.Replace("{{ "+spellVar.key+" }}", spellVar.Coeff[0].value.ToString());
            }
            var spellburn = spell.EffectBurn.Split(',').ToList();
            for (int i = 1; i < spellburn.Count; i++)
            {
                tooltip = tooltip.Replace("{{ e" + i + " }}", spellburn[i]);
            }

            builder.AddField("Tooltip",tooltip );
            builder.AddField("Stats", $"**Resource: **{spell.costType}\n" +
                                      $"**Costs: **{spell.resource.Replace("{{ cost }}", "40")}\n" +
                                      $"**Cooldown: **{spell.cooldownBurn}");
            
            await ReplyAsync("", embed: builder.Build());
        }

        [Command("game")]
        public async Task Game(string region, [Remainder] string summonerName)
        {
            Platforms platform = (Platforms)Enum.Parse(typeof(Platforms), region.ToUpper());
            var riotClient = new RiotClient(OptionManager.RiotKey);
            var champions = new RiotData().Champions;
            var summoner = riotClient.Summoner.GetSummonerByName(summonerName, platform);
            var match = riotClient.Specate.GameBySummoner(platform, summoner.SummonerId);
            var builder = Builders.BaseBuilder("", "", Color.DarkBlue,
                new EmbedAuthorBuilder().WithName(summonerName + "'s game"), "");
            TimeSpan time = new TimeSpan(0, 0, (int) match.GameLength);
            builder.AddField($"Information", $"**Map: **{match.MapId}\n**Time: **{Math.Round(time.TotalMinutes,2)} minutes\n**Mode: **{match.GameMode}");
            for (int i = 1; i < 3; i++)
            {
                string bans1 = "";
                foreach (var matchBannedChampion in match.BannedChampions)
                {
                    if (matchBannedChampion.TeamId == i*100)
                    {
                        try
                        {
                            bans1 +=
                                champions.FirstOrDefault(x => x.ChampionId == matchBannedChampion.ChampionId).name +
                                ", ";
                        }
                        catch
                        {
                            bans1 += "None, ";
                        }
                    }
                }
                bans1 = bans1.Remove(bans1.Length - 2, 2);
                builder.AddField("Bans Team "+i,
                    bans1);
                string names = "";
                string championsNames = "";
                foreach (var currentGameParticipant in match.Participants.Where(x => x.TeamId == i*100).ToList())
                {
                    names += currentGameParticipant.SummonerName + "\n";
                    championsNames += champions.FirstOrDefault(x=> x.ChampionId == currentGameParticipant.ChampionId).name + "\n";
                }
                builder.AddInlineField("Summoners", names);
                builder.AddInlineField("Champion", championsNames);
            }
            await ReplyAsync("", embed: builder.Build());
        }
        [Command("summoner")]
        public async Task Profile(string region, [Remainder] string summonerName)
        {
            var riotClient = new RiotClient(OptionManager.RiotKey);
            Platforms platform = (Platforms) Enum.Parse(typeof(Platforms), region.ToUpper());
            var summoner = riotClient.Summoner.GetSummonerByName(summonerName,platform);
            var leagues = riotClient.League.GetPositionDto(platform, summoner.SummonerId);
            var masteries = riotClient.Masteries.GetchampionMasteries(platform, summoner.SummonerId);
            var matches = 
            masteries = masteries.OrderByDescending(x => x.Level).ThenByDescending(x=> x.ChampionPoints).ToList();
            var builder = Builders.BaseBuilder("", "", Color.DarkBlue, new EmbedAuthorBuilder().WithName(summoner.Name),
                $"http://ddragon.leagueoflegends.com/cdn/6.24.1/img/profileicon/{summoner.ProfileIconId}.png");
            builder.AddInlineField($"Information", $"**Name: **{summoner.Name}\n**Region: **region\n**Level: **{summoner.SummonerLevel}");
            string rankings = "";
            foreach (var leaguePositionDto in leagues)
            {
                rankings += $"**{leaguePositionDto.QueueType}: **{leaguePositionDto.Tier} {leaguePositionDto.Rank}\n";
            }
            if (rankings == "") rankings = "Unranked";
            builder.AddInlineField($"Ranking", rankings);
            int champions = 4;
            if (champions > masteries.Count) champions = masteries.Count;
            builder.AddField("Top Mastery Champions", $"The top {champions} champions by mastery score");
            for (int i = 0; i < champions; i++)
            {
                int id = Convert.ToInt32(masteries[i].ChampionId);
                var champion = new RiotData().Champions.FirstOrDefault(x=> x.ChampionId == id);
                builder.AddInlineField(champion.name, $"**Level: **{masteries[i].Level}\n**Points: **{masteries[i].ChampionPoints}");
            }
            builder.AddField("Top Ranked Champions", "The top 3 champions by ranked played games");
            builder.AddInlineField("Champ1", "**Games: **X\n**Average Stats:** K/D/A\n**Winrate: **XX%");
            builder.AddInlineField("Champ2", "**Games: **X\n**Average Stats:** K/D/A\n**Winrate: **XX%");
            builder.AddInlineField("Champ3", "**Games: **X\n**Average Stats:** K/D/A\n**Winrate: **XX%");
            builder.AddInlineField("Champ4", "**Games: **X\n**Average Stats:** K/D/A\n**Winrate: **XX%");
            await ReplyAsync("", embed: builder.Build());
        }

        [Command("mastery")]
        public async Task Mastery(string region, string summonerName, string championName)
        {
            RiotData data = new RiotData();
            DataLibrary.Champion champion = null;
            Discord.EmbedBuilder builder = null;
            var championList = data.Champions.Where(x=> x.name.ToLower().Equals(championName.ToLower())).ToList();
            if (championList.Count < 1)
            {
                championList = new RiotData().Champions.Where(x => x.name.ToLower().Contains(championName.ToLower()))
                    .ToList();
            }
            champion = championList[0];
            if (champion != null)
            {
                builder = Builders.BaseBuilder($"{summonerName} mastery for {champion.name}", "", Color.DarkBlue, null, $"http://ddragon.leagueoflegends.com/cdn/6.24.1/img/champion/{champion.key}.png");
                builder.AddInlineField("Level", "X");
                builder.AddInlineField("Points", "XXXXXXXXXXXX");
            }
            else
            {
                builder = Builders.ErrorBuilder("Champion was not found");
            }

            await ReplyAsync("", embed: builder.Build());
        }

        [Command("mastery")]
        public async Task Mastery(string champion)
        {
            await ReplyAsync("Only using build in account");
        }
    }
}

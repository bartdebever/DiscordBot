using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampionGGHandler;
using Discord.Commands;
using DiscordBot.EmbedBuilder;

namespace DiscordBot.Modules
{
    [Group("Championgg")]
    public class ChampionGGModule : ModuleBase
    {
        [Command("Search")]
        public async Task Stats([Remainder] int id)
        {
            var stats = RequestHandler.GetChampionDataById(id);
            await ReplyAsync("", embed: ChampionGGBuilder.GetChampionInfo(stats[0]).Build());
        }
    }
}

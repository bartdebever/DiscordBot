using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampionGGHandler;
using Discord.Commands;

namespace DiscordBot.Modules
{
    [Group("Championgg")]
    public class ChampionGGModule : ModuleBase
    {
        [Command("Search")]
        public async Task Stats([Remainder] int id)
        {
            var stats = RequestHandler.GetChampionDataById(id);
            await ReplyAsync($"**ELO: **{stats[0].Elo}");
        }
    }
}

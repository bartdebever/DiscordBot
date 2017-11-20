using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordBot.EmbedBuilder;

namespace DiscordBot.Modules
{
    [Group("Smash")]
    public class SmashModule : ModuleBase
    {
        [Command("Info")]
        public async Task Info([Remainder, Summary("Username of the user needing to be queries")] string username)
        {
            /* TODO
             * Lookup user in database.
             * Find user
             * Throw error if not succeeded.
             */
            if (username == "error")
            {

            }
            else
            {
                await ReplyAsync("", embed: SmashBuilder.UserInfo(username));
            }
            
        }
    }

    [Group("Melee")]
    public class MeleeModule : ModuleBase
    {
        
    }
}

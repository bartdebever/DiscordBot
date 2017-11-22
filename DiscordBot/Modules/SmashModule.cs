using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;
using KugorganeHammerHandler;

namespace DiscordBot.Modules
{
    [Group("Smash")]
    public class SmashModule : ModuleBase
    {
        [Group("WiiU")]
        public class WiiU : ModuleBase
        {
            [Command("")]
            public async Task GetCharacter([Remainder] string name)
            {
                var character = RequestHandler.GetCharacterName(name);
                Discord.EmbedBuilder builder = Builders.BaseBuilder("", "", Color.DarkTeal,
                    new EmbedAuthorBuilder().WithName("KuroganeHammer Result:").WithUrl("http://kuroganehammer.com"), character.ThumbnailURL);
                builder.WithImageUrl(character.MainImageURL);
                builder.WithUrl(character.FullURL);
                await ReplyAsync("", embed: builder.Build());
            }
        }
    }
}

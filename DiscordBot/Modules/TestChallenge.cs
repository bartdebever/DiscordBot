using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;

namespace DiscordBot.Modules
{
    [Group("Challenge")]
    public class TestChallenge : ModuleBase
    {
        [Command("accept")]
        public async Task Accept(IUser user)
        {
            var message  = await ReplyAsync("Getting stage list");
            var builder = Builders.BaseBuilder($"GAMENAME: {Context.User.Username} vs {user.Username}", "",
                Color.DarkRed, null, "");
            builder.AddInlineField("Starters",
                "-Stage\n" +
                "-Stage\n" +
                "-Stage\n" +
                "-Stage\n" +
                "-Stage");
            builder.AddInlineField("Counter Picks",
                "~~-Stage~~\n" +
                "~~-Stage~~\n");
            builder.AddField("How to Ban", "\nUser -ch ban *<stage>* to ban a stage.");
            await ReplyAsync("", embed: builder.Build());
            await message.DeleteAsync();
        }
    }
}

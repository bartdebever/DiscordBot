using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Static_Data;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.EmbedBuilder;

namespace DiscordBot.Modules
{
    [Group("help")]
    public class HelpModule : ModuleBase
    {
        [Command("")]
        public async Task GetHelp()
        {
            var builder = Builders.BaseBuilder("All commands known", "", Color.DarkerGrey, null, "");
            foreach (var module in DiscordManager.Commands.Modules)
            {
                string commandinfo = "";
                foreach (var moduleCommand in module.Commands)
                {
                    string arguments = "";
                    foreach (var moduleCommandParameter in moduleCommand.Parameters)
                    {
                        arguments += $"<{moduleCommandParameter.Name}> ";
                    }
                    commandinfo += $"**-{module.Name} {moduleCommand.Name} {arguments}: **{moduleCommand.Summary}\n";
                }
                if(!string.IsNullOrEmpty(commandinfo) && !string.IsNullOrEmpty(module.Name))
                builder.AddInlineField(module.Name, commandinfo);
            }
            await ReplyAsync("", embed: builder.Build());
        }
    }
}

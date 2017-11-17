using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Discord.Implemented;
using DataLibrary.Static_Data;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.EmbedBuilder;

namespace DiscordBot.Modules
{
    [Group("Server")]
    [Summary("TODO")]
    public class ServerModule : ModuleBase
    {
        [Command("")]
        [Summary("Display information about the current server.")]
        public async Task Info()
        {
            await ReplyAsync("", embed:Builders.CurrentServerEmbed(Context.Guild));
        }

        [Command("register")]
        [Summary("Registers the server.")]
        //Requirement, user is the owner
        public async Task Register([Remainder, Summary("The name the server should be known by.")]string name)
        {
            
            DiscordUser owner = null;
            var database = DatabaseManager.GetMock();
            try { owner = database.Users.FirstOrDefault(x=>x.Discordid == (long) Context.User.Id); } catch { }
            if (owner != null)
            {
                database.Servers.Add(new DiscordServer(0,name, owner, (long) Context.Guild.Id, DateTime.Now));
            }
            database.SaveChanges();
            await ReplyAsync("Server has been saved, use -server to view its information");

        }

        [Command("list")]
        [Summary("Gives an ordered list of all the servers known to AtlasBot")]
        public async Task List([Optional, Summary("The name to be filtered by")] string filter)
        {
            await ReplyAsync("", embed: Builders.ServerList(filter));
        }
    }
}

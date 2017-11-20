using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Discord.Implemented;
using DataLibrary.Static_Data;
using DataLibrary.Useraccounts.Implementation;
using DataLibrary.Useraccounts.Interfaces;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;
using DiscordBot.Loggers;
using IUser = DataLibrary.Useraccounts.Interfaces.IUser;

namespace DiscordBot.Modules
{
    [Group("User"), Summary("Commands to manage your user profile.")]
    public class UserModule : ModuleBase
    {
        private Mock database = DatabaseManager.GetMock();
        [Command(""), Summary("Give information about the currect user")]
        public async Task UserInfo([Optional, Summary("The username of the user who you want to check out.")] string name)
        {
            if (string.IsNullOrEmpty(name))
            { 
                await ReplyAsync("", embed: Builders.CurrentUserEmbed(Context));
            }
            else await ReplyAsync("Information about " + name);
            await DefaultLogger.Logger(new LogMessage(LogSeverity.Info, Context.Guild.Name,
                Context.User.Username + " viewed information"));
        }
        [Command("Register"), Summary("Register using this command. Optionally you can give a username.")]
        public async Task Register([Optional, Summary("The username that the user wants to register with")] string name)
        {
            if (!string.IsNullOrEmpty(name)) await ReplyAsync("Registered with Tier \"" + name + "\"");
            else
            {
                    if (database.Users.FirstOrDefault(x => x.Discordid == (long) Context.User.Id) == null)
                    {
                        var user = database.Users.Add(new DiscordUser(DatabaseManager.UserKeyGenerator(), Context.User.Username, new List<ISummoner>(),
                            DateTime.Now, new SmashAccount()));
                        user.Discordid = (long) Context.User.Id;
                        database.SaveChanges();
                    }
                
                await ReplyAsync("Registered with Discord Name: " + Context.User.Username);
            }
        }

        [Group("Tier")]
        public class Username : ModuleBase
        {
            [Command("")]
            [Summary("Echos your current username back to you.")]
            public async Task Echo()
            {
                await ReplyAsync("Username from database");
            }

            [Command("change")]
            [Summary("Change your username to a new one")]
            public async Task Change(string name)
            {
                if (!string.IsNullOrEmpty(name)) await ReplyAsync("Name set to: " + name);
            }
        }
    }
}

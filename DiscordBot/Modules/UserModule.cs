using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Discord.EmbedBuilder builder = null;
            string username = Context.User.Username;
            if (!string.IsNullOrEmpty(name)) username = name;
            if (database.Users.FirstOrDefault(x => x.Discordid == (long)Context.User.Id) == null)
            {
                var user = database.Users.Add(new DiscordUser(DatabaseManager.UserKeyGenerator(),
                    username, new List<ISummoner>(),
                    DateTime.Now, new SmashAccount()));
                user.Discordid = (long)Context.User.Id;
                database.SaveChanges();
                builder = Builders.BaseBuilder("", "", ColorPicker.UserModule,
                    new EmbedAuthorBuilder().WithName("Successfully registered account."), "");
                builder.AddField("New " + Names.Systemname + " account registed",
                    "Successfully registerd a new account with the name \"" + user.Name +
                    "\"\nUser **-user** to get information about your account, use **-HELP COMMAND** to get info on what to do now");
            }
            else
            {
                builder = Builders.ErrorBuilder("User is already registered.\nUse **-user** to view your info");
            }
            await ReplyAsync("", embed: builder.Build());
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

        [Command("Anther")]
        public async Task SmashInfo([Required] IGuildUser user)
        {
            Mock database = DatabaseManager.GetMock();
            DiscordUser discordUser = null;
            Discord.EmbedBuilder builder;
            discordUser = database.Users.FirstOrDefault(x => x.Discordid == (long) user.Id);
            if (discordUser == null)
            {
                builder = Builders.ErrorBuilder("User does not have a " + Names.Systemname + " account.\nUser -user register to register now!");
            }
            else if (discordUser.SmashAccount.Username == null)
            {
                builder = Builders.ErrorBuilder("User does not have a " + Names.SmashLadder + " account.\nUser -Anther register *<name>* to start getting set up.");
            }
            else
            {
                builder = SmashBuilder.UserInfo(discordUser.SmashAccount.Username);
            }
            try
            {
                await ReplyAsync("", embed: builder.Build());
            }
            catch
            {
                await ReplyAsync("", embed: Builders.ErrorBuilder("Critical unknown error!").Build());
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Discord.Implemented;
using DataLibrary.Static_Data;
using DataLibrary.Useraccounts.Implementation;
using Discord.Commands;
using DiscordBot.EmbedBuilder;
using SmashHandler;
using SmashHandler.DataTypes;

namespace DiscordBot.Modules
{
    [Group("Anther")]
    public class AntherModule : ModuleBase
    {
        [Command("Info")]
        public async Task Info([Remainder, Summary("Username of the user needing to be queries")] string username)
        {
            try
            {
                await ReplyAsync("", embed: SmashBuilder.UserInfo(username));
            }
            catch
            {
                await ReplyAsync("", embed: Builders.ErrorBuilder("User \""+ username + "\" was not found. \nPlease try to look it up on " + Names.SmashLadder));
            }
            
        }

        [Command("Register")]
        public async Task Register([Remainder, Summary("Username of the user wanting to register")] string username)
        {
            RootObject root = RequestHandler.GetUserByName(username);
            User user = root.user;
            Mock database = DatabaseManager.GetMock();
            DiscordUser atlasUser = database.Users.FirstOrDefault(x => x.Discordid == (long) Context.User.Id);
            if (atlasUser.SmashAccount.Username == null)
            {
                SmashAccount account = new SmashAccount(user.username, "RANDOMTOKEN", false);
                atlasUser.SmashAccount = account;
                DatabaseManager.GetMock().SaveChanges();
                await ReplyAsync("Added your account!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Discord.Implemented;
using DataLibrary.Static_Data;
using DataLibrary.Useraccounts.Implementation;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;
using DiscordBot.Helper;
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

        [Command("Update")]
        public async Task Update()
        {
            Mock database = DatabaseManager.GetMock();
            DiscordUser atlasUser = database.Users.FirstOrDefault(x => x.Discordid == (long)Context.User.Id);
            var ranks = RequestHandler.GetRanksByName(atlasUser.SmashAccount.Username);
            foreach (var rank in ranks)
            {
                IRole role = RoleAssignment.DynamicRole(rank, Context.Guild);
                await (Context.User as IGuildUser).AddRoleAsync(role);
            }
            await ReplyAsync("Gave you your role!");
        }

        [Command("Ranks")]
        public async Task Ranks()
        {
            Mock database = DatabaseManager.GetMock();
            DiscordUser atlasUser = database.Users.FirstOrDefault(x => x.Discordid == (long)Context.User.Id);
            var ranks = RequestHandler.GetRanksByName(atlasUser.SmashAccount.Username);
            if (ranks.Count > 0)
            {
                string result = atlasUser.Name + "Anther's Ladder's rankings\n";
                foreach (var rank in ranks)
                {
                    result += rank + "\n";
                }
                await ReplyAsync(result);
            }
            else
            {
                await ReplyAsync("You do not have any rankings");
            }

        }

        [Command("Games")]
        public async Task Games()
        {
            var games = RequestHandler.GetGames();
            Discord.EmbedBuilder builder = Builders.BaseBuilder("", "", ColorPicker.SmashModule,
                null, "");
            string message = "";
            games.ForEach(x=> message+= x.Name + "\n");
            builder.AddField(new EmbedFieldBuilder().WithValue(message).WithName("All games playable on "+Names.SmashLadder));
            await ReplyAsync("", embed:builder.Build());
        }

        [Command("Character")]
        public async Task Characters()
        {
            var characters = RequestHandler.GetCharacters();
            await ReplyAsync(characters.Count.ToString());
        }
    }
}

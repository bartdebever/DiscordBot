using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Image_generator;

namespace DiscordBot.Modules
{
    [Group("Test")]
    public class TestModule : ModuleBase
    {
        [Command("1"), Summary("A internal test command")]
        public async Task Say([Remainder, Summary("Var1")] string var)
        {

            await Context.Channel.SendFileAsync(SmashPlayerCard.Test(), "You're player card");
        }

        [Command("2")]
        public async Task SayMultiple([Optional] string var1, [Optional] string var2, [Remainder] string var3)
        {
            await ReplyAsync(var1);
            await ReplyAsync(var2);
            await ReplyAsync(var3);
            
        }

        [Command("3")]
        public async Task SayUserInfo(SocketUser user = null)
        {
            if (user == null)
            {
                user = Context.Message.Author as SocketUser;
            }
            
            await ReplyAsync(user.GetAvatarUrl());
        }

        [Command("4")]
        public async Task SayServerInfo()
        {
            await ReplyAsync(Context.Guild.Name);
        }

        [Command("5")]
        public async Task AddRole(string role)
        {
            await (Context.User as IGuildUser).AddRoleAsync(
                Context.Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == role.ToLower()));
            await ReplyAsync("Given the " + role + " to " + Context.User.Username);
        }
    }
}

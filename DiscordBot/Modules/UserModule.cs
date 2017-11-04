using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Static_Data;
using Discord;
using Discord.Commands;

namespace DiscordBot.Modules
{
    [Group("User"), Summary("Commands to manage your user profile.")]
    public class UserModule : ModuleBase
    {
        [Command(""), Summary("Give information about the currect user")]
        public async Task UserInfo([Optional, Summary("The username of the user who you want to check out.")] string name)
        {
            bool registered = false;
            if (string.IsNullOrEmpty(name))
            {
                
                EmbedBuilder eb = new EmbedBuilder();
                eb.Author = new EmbedAuthorBuilder().WithName("Information about " + Context.User.Username).WithIconUrl(Context.User.GetAvatarUrl());
                eb.WithCurrentTimestamp();
                //eb.WithUrl("http://placeholder.com/user/id/111020301023");
                eb.WithColor(Color.Blue);
                eb.AddField(new EmbedFieldBuilder().WithValue(Context.User.Status).WithName("Status").WithIsInline(false));
                try
                {
                    var user = Context.User as IGuildUser;
                    eb.AddField(new EmbedFieldBuilder().WithName("Joined " + Context.Guild.Name + " at")
                        .WithValue(user.JoinedAt.Value.DateTime.ToLongDateString()));
                }
                catch 
                {
                    //Log User not in guild
                }

                if (Context.User.Game != null)
                {
                    eb.AddField(new EmbedFieldBuilder().WithValue(Context.User.Game.Value.Name)
                        .WithName("Currently Playing"));
                }
                if (registered)
                {
                    //Display info about registered user
                }
                else
                {
                    eb.AddField(new EmbedFieldBuilder()
                        .WithValue("User is not registed, use -user register to register now.")
                        .WithName(Names.Systemname + " Account"));
                }
                eb.Footer = new EmbedFooterBuilder().WithText(Names.BotName + " || " + DateTime.Now.ToLongDateString()).WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl());
                await ReplyAsync("", embed: eb.Build());
            }
            else await ReplyAsync("Information about " + name);
        }
        [Command("Register"), Summary("Register using this command. Optionally you can give a username.")]
        public async Task Register([Optional, Summary("The username that the user wants to register with")] string name)
        {
            if (!string.IsNullOrEmpty(name)) await ReplyAsync("Registered with name \"" + name + "\"");
            else await ReplyAsync("Registered with Discord Name: " + Context.User.Username);
        }

        [Group("name")]
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

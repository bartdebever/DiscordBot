using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Discord.Implemented;
using DataLibrary.Static_Data;
using Discord;
using Discord.Commands;
using IUser = DataLibrary.Useraccounts.Interfaces.IUser;

namespace DiscordBot.EmbedBuilder
{
    public static class Builders
    {
        public static Embed CurrentUserEmbed(ICommandContext Context)
        {
            bool registered = false;
            Mock database = DatabaseManager.GetMock();
            IUser atlasAccount = null;
            try
            {
                atlasAccount =
                    database.Users.FirstOrDefault(x => (x as DiscordUser).Discordid == (long)Context.User.Id);
                registered = true;
            }
            catch
            {
                //Log user not registed
            }
            Discord.EmbedBuilder eb = new Discord.EmbedBuilder();
            if (registered)
            {
                eb.Description = "Displaying user information for the " + Names.Systemname + " account";
                eb.AddField(new EmbedFieldBuilder().WithName("Name").WithValue(atlasAccount.Name));
                eb.AddField(new EmbedFieldBuilder().WithName("Account created")
                    .WithValue(atlasAccount.CreationDate.ToLongDateString()));
            }
            else
            {
                eb.AddField(new EmbedFieldBuilder()
                    .WithValue("User is not registed, use -user register to register now.")
                    .WithName(Names.Systemname + " Account"));
            }
            eb.Author = new EmbedAuthorBuilder().WithName("Information about " + Context.User.Username);
            //eb.WithTitle("Information about " + Context.User.Username);
            eb.WithCurrentTimestamp();
            //eb.WithUrl("http://placeholder.com/user/id/111020301023");
            eb.WithColor(Color.Blue);
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
            eb.AddField(new EmbedFieldBuilder().WithValue(Context.User.Status).WithName("Status").WithIsInline(false));
            if (Context.User.Game != null)
            {
                eb.AddField(new EmbedFieldBuilder().WithValue(Context.User.Game.Value.Name)
                    .WithName("Currently Playing"));
            }

            eb.WithThumbnailUrl(Context.User.GetAvatarUrl());
            eb.Footer = new EmbedFooterBuilder().WithText(Names.BotName).WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl());
            return eb.Build();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary;
using DataLibrary.Discord.Implemented;
using DataLibrary.Interfaces;
using DataLibrary.Static_Data;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
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
                
            }
            catch
            {
                //Log user not registed
            }
            Discord.EmbedBuilder eb = new Discord.EmbedBuilder();
            if (atlasAccount != null)
            {
                eb.Description = "Displaying user information for the " + Names.Systemname + " account";
                eb.AddField(new EmbedFieldBuilder().WithName(Names.Systemname + " Information").WithValue(
                    "**Name:** " + atlasAccount.Name + "\n"+
                    "**Account created:** " + atlasAccount.CreationDate.ToLongDateString()));
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
            string discordInformation = "";
            try
            {
                var user = Context.User as IGuildUser;
                discordInformation += "**Joined at: **" + user.JoinedAt.Value.Date.ToLongDateString() + "\n";
            }
            catch
            {
                //Log User not in guild
            }
            discordInformation += "**Status:** " + Context.User.Status + "\n";

            if (Context.User.Game != null)
            {
                discordInformation += "**Currently Playing: **" + Context.User.Game.Value.Name + "\n";
            }
            eb.AddField(new EmbedFieldBuilder().WithName("Discord information").WithValue(discordInformation));
            eb.WithThumbnailUrl(Context.User.GetAvatarUrl());
            eb.Footer = new EmbedFooterBuilder().WithText(Names.BotName).WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl());
            return eb.Build();
        }

        public static Embed CurrentServerEmbed(IGuild guild)
        {
            DiscordServer server;
            var database = DatabaseManager.GetMock();
            try
            {
                server = database.Servers.FirstOrDefault(x => x.ServerId == (long) guild.Id);
            }
            catch
            {
                server = null;
            }
            
            Discord.EmbedBuilder builder = new Discord.EmbedBuilder();
            try
            {
                builder.ThumbnailUrl = guild.IconUrl;
            }
            catch
            {
                //Server does not have an icon.
            }
            builder.Author = new EmbedAuthorBuilder().WithName(guild.Name);
            builder.Description = "Information about this server known to " + Names.BotName;
            builder.AddField(new EmbedFieldBuilder().WithName("Discord Information")
                .WithValue("**Name:** " + guild.Name + "\n"
                           + "**Users:** " + (guild as SocketGuild).Users.Count + "\n"
                           + "**Created at:** " + guild.CreatedAt.DateTime.ToLongDateString() + "\n"
                           + "**Owner:** " + guild.GetOwnerAsync().Result.Username));
            if (server == null)
            {
                builder.AddField(new EmbedFieldBuilder().WithName(Names.Systemname + " Information")
                    .WithValue(
                        "Server is not linked to an account. Let the owner use -server register *<name>* to start"));
            }
            else
            {
                builder.AddField(new EmbedFieldBuilder().WithName(Names.Systemname + " Information").WithValue(
                    "**Name: **" + server.Name+"\n"+
                    "**Description: **"+ server.Description+"\n"+
                    "**Joined at: **" + server.JoinDateTime.ToLongDateString()));
            }
            builder.WithColor(Color.Blue);
            builder.WithCurrentTimestamp();
            return builder.Build();
        }
    }
}

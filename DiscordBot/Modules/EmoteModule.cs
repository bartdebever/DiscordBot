using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Static_Data;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;

namespace DiscordBot.Modules
{
    [Group("Emote")]
    public class EmoteModule : ModuleBase
    {
        [Command("Track")]
        public async Task TrackMessage(ulong messageId)
        {
            ReactionTracker.SubscribeMessage(messageId);
            await ReplyAsync("Subscribed Message");
        }

        [Command("Result")]
        public async Task Result(ulong messageId)
        {
            ReactionList reactions = ReactionTracker.GetReactionList(messageId);
            Discord.EmbedBuilder builder = null;
            if (reactions != null)
            {
                builder = Builders.BaseBuilder("Reactions stats for the message", "", Color.Orange, null, "");
                List<string> emotes = new List<string>();
                foreach (var reactionsReaction in reactions.Reactions)
                {
                    if (!emotes.Contains(reactionsReaction.Emoji))
                    {
                        emotes.Add(reactionsReaction.Emoji);
                    }
                }
                foreach (var emote in emotes)
                {
                    string info = "";
                    reactions.Reactions.Where(x => x.Emoji == emote).ToList()
                        .ForEach(x => info += x.Date.ToShortDateString() + " " +x.Date.ToShortTimeString() + "\n");
                    builder.AddInlineField(emote, info);
                }
            }
            else
            {
                builder = Builders.ErrorBuilder("Message was not tracked!");
            }
            await ReplyAsync("", embed: builder.Build());
        }

    }
}

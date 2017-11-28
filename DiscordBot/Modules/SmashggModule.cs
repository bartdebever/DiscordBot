using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;
using SmashGgHandler;

namespace DiscordBot.Modules
{
    [Group("smashgg")]
    public class SmashggModule : ModuleBase
    {
        [Command("tournament")]
        public async Task GetInfo([Remainder] string name)
        {
            var root = RequestHandler.GetTournamentRoot(name);
            if (root != null)
            {
                if (!root.entities.tournament.Private)
                {
                    var tournament = root.entities.tournament;
                    var builder = Builders.BaseBuilder(tournament.name, "", Color.DarkGreen, null,
                        null);
                    var baseDate = new DateTime(1970, 1, 1, 0, 0, 0);
                    var startDate = baseDate.AddSeconds(tournament.startAt);
                    var endDate = baseDate.AddSeconds(tournament.endAt);
                    builder.AddField("Information",
                        $"**Name: **{tournament.name}\n" +
                        $"**Venue: **{tournament.venueName}\n" +
                        $"**Venue Adress: **{tournament.venueAddress}\n" +
                        $"**Timezone: **{tournament.timezone.Replace("/", " ").Replace("_", "")}\n" +
                        $"**From **{startDate.ToLongDateString()} \n" +
                        $"**To:** {endDate.ToLongDateString()}");
                    var gameInfo = "";
                    foreach (var game in root.entities.videogame)
                    {
                        var events = root.entities.Event.Where(x => x.videogameId == game.id).ToList();
                        string info = "";
                        events.ForEach(x => info += "- " + x.name + "\n");
                        //builder.AddField(game.name + " Events",
                            //info);
                        gameInfo += $"**{game.name}:**\n" +
                                    $"{info}";
                    }
                    builder.AddField("Events", gameInfo);
                    var image = tournament.images.FirstOrDefault(x => x.type == "profile");
                    if (image != null) builder.WithThumbnailUrl(image.url);
                    var banner = tournament.images.FirstOrDefault(x => x.type == "banner");
                    if (banner != null) builder.ImageUrl = banner.url;
                    await ReplyAsync("", embed: builder.Build());
                }
                else
                {
                    //TODO tournament is private error
                }
            }
            else
            {
                //TODO Tournament not found error
            }

        }
    }
}

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
                        //gameInfo += $"**{game.name}:**\n" +
                        //            $"{info}";
                        builder.AddInlineField(game.name, info);
                    }
                    //builder.AddField("Events", gameInfo);
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

        [Command("result")]
        public async Task GetResult([Remainder] string name)
        {
            Discord.EmbedBuilder builder = null;
            var tournament = RequestHandler.GetTournamentRoot(name);
            var bracketPhases = tournament.entities.phase.Where(x => x.groupCount == 1).ToList();
            var icon = tournament.entities.tournament.images.FirstOrDefault(x => x.type == "profile");
            string url = "";
            if (icon != null) url = icon.url;
            builder = Builders.BaseBuilder(tournament.entities.tournament.name, "", Color.DarkOrange, null, url);
            foreach (var bracketPhase in bracketPhases)
            {
                var phaseId = bracketPhase.id;
                var selectedEvent = tournament.entities.Event.FirstOrDefault(x => x.id == bracketPhase.eventId);
                var group = tournament.entities.groups.FirstOrDefault(x => x.phaseId == phaseId);
                var result = RequestHandler.GetResult(group.id);
                var players = result.Entities.Player;
                if (players != null)
                {
                    var seedlist = result.Entities.Seeds.Where(x => x.Placement != null).ToList();
                    var seeds = seedlist.OrderBy(x => x.Placement).ToList(); //Results sorted by ranking
                    int playerCount = 10;
                    if (seeds.Count < playerCount) playerCount = seeds.Count;
                    string placementInfo = "";
                    for (int i = 0; i < playerCount; i++)
                    {
                        var player = players.Where(x => Convert.ToInt64(x.EntrantId) == seeds[i].EntrantId).ToList();
                        if (player.Count == 2)
                        {
                            var player1 = "";
                            var player2 = "";
                            if (!string.IsNullOrEmpty(player[0].Prefix))
                            {
                                player1 = $"**{player[0].Prefix}** {player[0].GamerTag}";
                            }
                            else
                            {
                                player1 = $"{player[0].GamerTag}";
                            }
                            if (!string.IsNullOrEmpty(player[1].Prefix))
                            {
                                player2 = $"**{player[1].Prefix}** {player[1].GamerTag}";
                            }
                            else
                            {
                                player2 = $"{player[1].GamerTag}";
                            }

                            placementInfo +=
                                $"{seeds[0].Placement}: {player1} | {player2}\n";
                        }
                        else
                        {
                            var player1 = "";
                            if (!string.IsNullOrEmpty(player[0].Prefix))
                            {
                                player1 = $"**{player[0].Prefix}** {player[0].GamerTag}";
                            }
                            else
                            {
                                player1 = $"{player[0].GamerTag}";
                            }
                            if (player[0] != null) placementInfo += $"{seeds[i].Placement}: {player1}\n";
                        }
                        
                    }
                    if (!string.IsNullOrEmpty(placementInfo))
                        builder.AddInlineField($"{selectedEvent.name} Results", placementInfo);
                }

            }
            var tournamentDate = new DateTime(1970, 1, 1, 0, 0, 0);
            tournamentDate = tournamentDate.AddSeconds(tournament.entities.tournament.endAt);
            TimeSpan duration = DateTime.Now - tournamentDate;
            if ((duration.Days < 7 || duration.Days < 0) && Context.Guild != null)
            {
                var spoiled = Builders.BaseBuilder("Spoiler preventer!", "", Color.Red, null, "");
                spoiled.AddField("Spoiler prevented!",
                    "To prevent spoiling tournament results for people, we only allow tournaments older than a week to be shown in servers.\nThe embed has been DMed to you ;)");
                await ReplyAsync("", embed: spoiled.Build());
                await Context.Message.Author.GetOrCreateDMChannelAsync().Result
                    .SendMessageAsync("", embed: builder.Build());
            }
            else
            {
                await ReplyAsync("", embed: builder.Build());
            }
            
        }
    }
}

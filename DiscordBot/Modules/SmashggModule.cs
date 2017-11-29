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

        [Command("performance")]
        public async Task PlayerPerformance(string playerName, [Remainder] string tournamentName)
        {
            var tournament = RequestHandler.GetTournamentRoot(tournamentName);
            var bracketPhases = tournament.entities.phase.Where(x => x.groupCount == 1).ToList();
            Discord.EmbedBuilder builder = Builders.BaseBuilder(playerName + " Results For " + tournament.entities.tournament.name, "", Color.Red, null, "");
            foreach (var bracketPhase in bracketPhases)
            {
                var phaseId = bracketPhase.id;
                var selectedEvent = tournament.entities.Event.FirstOrDefault(x => x.id == bracketPhase.eventId);
                var group = tournament.entities.groups.FirstOrDefault(x => x.phaseId == phaseId);
                var result = RequestHandler.GetResultSets(group.id);
                var players = result.Entities.Player;
                var player = players.Where(x => x.GamerTag.ToLower().Trim().Contains(playerName.ToLower().Trim()))
                    .ToList();
                if (player.Count == 1) //player is found, lets go
                {
                    builder.WithTitle($"{player[0].GamerTag} Results at {tournament.entities.tournament.name}");
                    var sets = result.Entities.Sets.Where(x =>
                        x.Entrant1Id == Convert.ToInt64(player[0].EntrantId) ||
                        x.Entrant2Id == Convert.ToInt64(player[0].EntrantId)).ToList();
                    string setinfo = "";
                    sets = sets.OrderBy(x => x.CompletedAt).ToList();
                    var characters = new List<string>()
                    {
                        "Bowser",
                        "Captain Falcon",
                        "Donkey Kong",
                        "Dr. Mario",
                        "Falco",
                        "Fox",
                        "Ganondorf",
                        "Ice Climbers",
                        "Jigglypuff",
                        "Kirby",
                        "Link",
                        "Luigi",
                        "Mario",
                        "Marth",
                        "Mewtwo",
                        "Mr. Game And Watch",
                        "Ness",
                        "Peach",
                        "Pichu",
                        "Pikachu",
                        "Roy",
                        "Samus",
                        "Sheik",
                        "Yoshi",
                        "Young Link",
                        "Zelda"
                    };
                    foreach (var set in sets)
                    {
                        int playerPlace = 1;
                        Player player2 = null;
                        if (Convert.ToInt64(player[0].EntrantId) == set.Entrant1Id)
                            player2 = players.FirstOrDefault(x => set.Entrant2Id == Convert.ToInt64(x.EntrantId));
                        else
                        {
                            player2 = players.FirstOrDefault(x => set.Entrant1Id == Convert.ToInt64(x.EntrantId));
                            playerPlace = 2;
                        }
                        if (player2 != null)
                        {
                            string gameinfo = "";
                            foreach (var game in set.Games)
                            { 
                                gameinfo +=
                                    $"{characters[(int) game.Entrant1P1CharacterId - 1]} {game.Entrant1P1Stocks} - {game.Entrant2P1Stocks} {characters[(int) game.Entrant2P1CharacterId - 1]}\n";
                            }
                            if (gameinfo == "") gameinfo = "Nothing available";
                            if (playerPlace == 1 && set.Entrant1Score != null && set.Entrant2Score != null)
                                builder.AddField($"{player[0].GamerTag} - {player2.GamerTag}: {set.Entrant1Score} - {set.Entrant2Score}", gameinfo);
                            else if(set.Entrant1Score != null && set.Entrant2Score != null)
                                builder.AddField(
                                    $"{player2.GamerTag} - {player[0].GamerTag}: {set.Entrant1Score} - {set.Entrant2Score}",
                                    gameinfo);
                        }

                    }
                    //builder.AddField("Results", setinfo);
                }
            }
            await ReplyAsync("", embed: builder.Build());
        }
    }
}

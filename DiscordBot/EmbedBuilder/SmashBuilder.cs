using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Static_Data;
using Discord;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordBot.EmbedBuilder
{
    public static class SmashBuilder
    {
        public static Discord.EmbedBuilder UserInfo(string username) //TODO Change to object
        {
            CookieContainer cookieContainer = new CookieContainer();
            Uri uri = new Uri("http://www.smashladder.com");
            Cookie cookie =
                new Cookie("lad_sock_hash", "HASH", "/", "www.smashladder.com")
                {
                    Expires = new DateTime(2033, 05, 18, 03, 33, 22)
                };
            cookieContainer.Add(new Uri("http://www.smashladder.com"), cookie);
            cookieContainer.Add(uri, new Cookie("lad_sock_user_id", "78787", "/", "www.smashladder.com"));
            cookieContainer.Add(uri, new Cookie("lad_sock_remember_me", "bartdebever112%40outlook.com", "/", "www.smashladder.com"));
            HttpWebRequest webRequest =
                (HttpWebRequest)WebRequest.Create("http://www.smashladder.com/matchmaking/user?username=" + username);
            webRequest.CookieContainer = cookieContainer;
            webRequest.Method = "GET";
            string result;
            using (var reader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            RootObject root = JsonConvert.DeserializeObject<RootObject>(result);
            User user = root.user;
            Discord.EmbedBuilder builder = Builders.BaseBuilder("Link to " + Names.SmashLadder + " Profile", "All information known about **" + user.username + "**",
                ColorPicker.SmashModule,
                new EmbedAuthorBuilder().WithName(Names.SmashLadder)
                    .WithUrl("http://www.smashladder.com"), "");
            builder.WithThumbnailUrl("https://www.smashladder.com/images/subhypepikachu-logo.png");
            if (user.selected_flair != null)
            {
                builder.WithAuthor(new EmbedAuthorBuilder().WithName(Names.SmashLadder)
                    .WithUrl("http://www.smashladder.com").WithIconUrl("http:" + user.selected_flair.url));
            }
            string location = "";
            if (!string.IsNullOrEmpty(user.location.locality)) location += user.location.locality + ", ";
            if (!string.IsNullOrEmpty(user.location.state)) location += user.location.state + ", ";
            if (!string.IsNullOrEmpty(user.location.country.name)) location += user.location.country.name;
            builder.AddField(new EmbedFieldBuilder().WithName("Profile").WithValue(
                "**Name: **" + user.username + "\n" +
                "**Member Since: **" + user.member_since.full.ToLongDateString()+ "\n" +
                "**Location: **" + location+ "\n" +
                "**Status: **" + user.away_message + "\n" +
                "**Total matches played: **" + user.total_matches_played));
            List<Game> games = new List<Game>()
            {
                user.ladder_information.Melee,
                user.ladder_information.Brawl,
                user.ladder_information.ProjectM,
                user.ladder_information.RPS,
                user.ladder_information.SFF2,
                user.ladder_information.Smash3DS,
                user.ladder_information.Smash4Wiiu,
                user.ladder_information.Smash64
            };
            var filteredgames = games.Where(x => x != null).ToList();
            try
            {
                filteredgames = filteredgames.Where(x => x.league.stats.ranked_played != 0).ToList();
            }
            catch
            {
                //No ranked games played
            }
            foreach (var game in filteredgames)
                {
                    try
                    {
                        var characters = "";
                        var count = 3;
                        if (game.characters.Count < count) count = game.characters.Count;
                        if (count != 0)
                        {
                            characters = "**Characters:**\n";
                            for (int i = 0; i < count; i++)
                            {
                                characters += "**  " + game.characters[i].name + ": **" + game.characters[i].percent + "%\n";
                            }
                        }
                        var rank = "**Ranked: **Unranked\n";
                        if (!string.IsNullOrEmpty(game.league.name))
                        {
                            rank = "**Ranked: **" + game.league.name + " " + game.league.division + "\n";
                        }
                        builder.AddField(new EmbedFieldBuilder().WithName(game.name).WithValue(
                            "**Total ranked games played: **" + game.league.stats.ranked_played + "\n" +
                            rank+
                            characters
                        ));
                }
                    catch
                    {
                        
                    }

                }

            if (root.now_playing != null)
            {
                string type = "Friendlies";
                if (root.now_playing.is_ranked)
                {
                    type = "Ranked";
                }
                builder.AddField(new EmbedFieldBuilder().WithName("Currently Playing").WithValue(
                    "**" + root.now_playing.ladder_name + " " + type + "**: \n" +
                    //"Using " + root.now_playing.build.name
                    root.now_playing.player1.username + /* " " +(root.nowplay.player1.rank) + */ " vs " + root.now_playing.player2.username) /*+ " " (root.now_playing.player2.rank)*/);
            }
            builder.WithUrl(user.profile_url);
            return builder;
        }
    }

    public class Match
    {
        public string Type { get; set; }
        public string Game { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Winner { get; set; }

        public Match(string type, string game, string player1, string player2, string winner)
        {
            this.Type = type;
            this.Game = game;
            this.Player1 = player1;
            this.Player2 = player2;
            this.Winner = winner;
        }
    }
    public class Country
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Location
    {
        public List<double> coordinates { get; set; }
        public Country country { get; set; }
        public string state { get; set; }
        public string locality { get; set; }
        public string country_state { get; set; }
    }

    public class StatsSerialized
    {
        public double rating { get; set; }
        public int bonus_pool_used { get; set; }
        public int? id { get; set; }
    }

    public class Stats
    {
        public int? ranked_played { get; set; }
        public int? total_friendlies_played { get; set; }
        public int? unique_opponents { get; set; }
    }

    public class StartDate
    {
        public int timestamp { get; set; }
        public string courtesy { get; set; }
        public string full { get; set; }
    }

    public class EndDate
    {
        public int timestamp { get; set; }
        public string courtesy { get; set; }
        public string full { get; set; }
    }

    public class Season
    {
        public int id { get; set; }
        public string name { get; set; }
        public StartDate start_date { get; set; }
        public EndDate end_date { get; set; }
        public int games_required_for_ladder { get; set; }
        public int opponents_required_for_ladder { get; set; }
    }

    public class League
    {
        public string name { get; set; }
        public string division { get; set; }
        public StatsSerialized stats_serialized { get; set; }
        public Stats stats { get; set; }
        public int tier_rank { get; set; }
        public int division_number { get; set; }
        public string image_url { get; set; }
        public Season season { get; set; }
    }

    public class Build
    {
        public string download_file { get; set; }
        public string detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon_directory { get; set; }
        public string download_link { get; set; }
        public int? @default { get; set; }
        public object active { get; set; }
        public int is_currently_used { get; set; }
        public int dolphin_build_id { get; set; }
        public int order { get; set; }
        public int ladder_id { get; set; }
    }

    public class Game
    {
        public int id { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public string image_url { get; set; }
        public League league { get; set; }
        public List<Character> characters { get; set; }
    }

    public class StatsSerialized2
    {
        public int rating { get; set; }
        public int bonus_pool_used { get; set; }
        public object id { get; set; }
    }

    public class Stats2
    {
        public int ranked_played { get; set; }
        public object total_friendlies_played { get; set; }
        public int unique_opponents { get; set; }
    }

    public class StartDate2
    {
        public int timestamp { get; set; }
        public string courtesy { get; set; }
        public string full { get; set; }
    }

    public class EndDate2
    {
        public int timestamp { get; set; }
        public string courtesy { get; set; }
        public string full { get; set; }
    }

    public class Season2
    {
        public int id { get; set; }
        public string name { get; set; }
        public StartDate2 start_date { get; set; }
        public EndDate2 end_date { get; set; }
        public int games_required_for_ladder { get; set; }
        public int opponents_required_for_ladder { get; set; }
    }

    public class League2
    {
        public object name { get; set; }
        public StatsSerialized2 stats_serialized { get; set; }
        public Stats2 stats { get; set; }
        public int tier_rank { get; set; }
        public int division_number { get; set; }
        public string image_url { get; set; }
        public Season2 season { get; set; }
    }

    public class NintendoNetworkID
    {
        public object value { get; set; }
        public int type { get; set; }
    }

    public class HasDreamland
    {
        public string value { get; set; }
        public int type { get; set; }
    }
    public class __invalid_type__4
    {
        public int id { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public string image_url { get; set; }
        public League2 league { get; set; }
        public List<Character> characters { get; set; }
    }

    public class Character
    {
        public string name { get; set; }
        public int percent { get; set; }
    }
    public class LadderInformation
    {
        [JsonProperty(PropertyName = "1")]
        public Game ProjectM { get; set; }
        [JsonProperty(PropertyName = "2")]
        public Game Melee { get; set; }
        [JsonProperty(PropertyName = "3")]
        public Game Smash3DS { get; set; }
        [JsonProperty(PropertyName = "4")]
        public Game Smash4Wiiu { get; set; }
        [JsonProperty(PropertyName = "5")]
        public Game Smash64 { get; set; }
        [JsonProperty(PropertyName = "6")]
        public Game Brawl { get; set; }
        [JsonProperty(PropertyName = "7")]
        public Game SFF2 { get; set; }
        [JsonProperty(PropertyName = "8")]
        public Game RPS { get; set; }
    }

    public class MemberSince
    {
        public int timestamp { get; set; }
        public string courtesy { get; set; }
        public DateTime full { get; set; }
    }

    public class User
    {
        public object display_name { get; set; }
        public object max_tier_achievement { get; set; }
        public Location location { get; set; }
        public object is_subscribed { get; set; }
        public int id { get; set; }
        public string username { get; set; }
        public selected_flair selected_flair { get; set; }
        public object glow_color { get; set; }
        public bool is_admin { get; set; }
        public bool is_mod { get; set; }
        public string away_message { get; set; }
        public bool is_playing { get; set; }
        public bool is_online { get; set; }
        public string profile_url { get; set; }
        public string username_css_classes { get; set; }
        public List<object> reported_match_behavior { get; set; }
        public string local_time { get; set; }
        public LadderInformation ladder_information { get; set; }
        public int total_matches_played { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public List<object> notes { get; set; }
        public int is_ignored { get; set; }
        public int is_friend { get; set; }
        public bool has_dolphin_launcher { get; set; }
        public int subscription_streak { get; set; }
        public MemberSince member_since { get; set; }
        public Lobby now_playing { get; set; }
    }

    public class selected_flair
    {
        public string url { get; set; }
        public string safe_url { get; set; }
        public override string ToString()
        {
            return url;
        }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public User user { get; set; }
        public Lobby now_playing { get; set; }
    }
    public class Player
    {
        public int id { get; set; }
        public string username { get; set; }
    }

    public class Lobby
    {
        public bool is_ranked { get; set; }
        public int game_type { get; set; }
        public int ladder_id { get; set; }
        public string ladder_name { get; set; }
        public string ladder_slug { get; set; }
        public string game_slug { get; set; }
        public int is_disputed { get; set; }
        public Player player1 { get; set; }
        public Player player2 { get; set; }
    }
}

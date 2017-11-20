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
            DateTime birthday = new DateTime(2018, 2, 13);
            string lastSeen = "1h ago";
            string location = "Netherlands, North Brabant LOCAL";
            string bio = "Newish Dutch player wanting to improve";
            HttpResponseMessage response;
            CookieContainer cookieContainer = new CookieContainer();
            Uri uri = new Uri("http://www.smashladder.com");
            Cookie cookie =
                new Cookie("lad_sock_hash", "HASH HERE", "/", "www.smashladder.com")
                {
                    Expires = new DateTime(2033, 05, 18, 03, 33, 22)
                };
            cookieContainer.Add(new Uri("http://www.smashladder.com"), cookie);
            cookieContainer.Add(uri, new Cookie("lad_sock_user_id", "78787", "/", "www.smashladder.com"));
            cookieContainer.Add(uri, new Cookie("lad_sock_remember_me", "bartdebever112%40outlook.com", "/", "www.smashladder.com"));
            HttpWebRequest webRequest =
                (HttpWebRequest) HttpWebRequest.Create("http://www.smashladder.com/matchmaking/user?username=" + username);
            webRequest.CookieContainer = cookieContainer;
            webRequest.Method = "GET";
            string result;
            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            RootObject root = JsonConvert.DeserializeObject<RootObject>(result);
            User user = root.user;

                Discord.EmbedBuilder builder = Builders.BaseBuilder("Link to " + Names.SmashLadder + " Profile", "All information known about **" + username + "**",
                    ColorPicker.SmashModule,
                    new EmbedAuthorBuilder().WithName(Names.SmashLadder)
                        .WithUrl("http://www.smashladder.com"), "");
            builder.WithThumbnailUrl("https://www.smashladder.com/images/subhypepikachu-logo.png");
            if (user.selected_flair != null)
            {
                builder.WithAuthor(new EmbedAuthorBuilder().WithName(Names.SmashLadder)
                    .WithUrl("http://www.smashladder.com").WithIconUrl("http:" + user.selected_flair.url));
            }
            builder.AddField(new EmbedFieldBuilder().WithName("Profile").WithValue(
                "**Name: **" + user.username + "\n" +
                "**Member Since: **" + user.member_since.full+"\n"+
                "**Location: **"+ user.location.locality + ", "  +user.location.country_state+"\n"+
                "**Status: **" + user.away_message + "\n"+
                "**Total matches played: **" + user.total_matches_played));
            if (user.ladder_information.__invalid_name__2 != null)
            {
                var melee = user.ladder_information.__invalid_name__2;
                int characters = 3;
                string charactersinfo = "";
                if (melee.characters.Count < 3) characters = melee.characters.Count;
                for(int i = 0; i<characters; i++)
                {
                    charactersinfo += "**  " + melee.characters[i].name + ": **" + melee.characters[i].percent + "%" + "\n";
                }
                builder.AddField(new EmbedFieldBuilder().WithName(user.ladder_information.__invalid_name__2.name)
                    .WithValue(
                        "**Total games played: **" + melee.league.stats.ranked_played + "\n" +
                        "**Rank: **" + melee.league.name + " " + melee.league.division + "\n" +
                        "**Character info:** displayed as Name: percentage played \n" + charactersinfo));
            }
            
            builder.WithUrl(user.profile_url);
            //General information

            //builder.AddField(new EmbedFieldBuilder().WithName("Profile").WithValue(
            //    "**Name:** " + user.username + "\n"+
            //    "**Birthday:** " + birthday.ToString("MMMM", CultureInfo.InvariantCulture)  +" "  + birthday.Day +"\n"+
            //    "**Last Seen:** "+lastSeen+"\n"+
            //    "**Location:** " + user.location.country_state));
            //builder.AddField(new EmbedFieldBuilder().WithName("About " + username).WithValue(
            //    bio));
            ////TODO Check if player plays melee
            //builder.AddField(new EmbedFieldBuilder().WithName("Super Smash Bros Melee")
            //    .WithValue("**Mains:** Marth, Fox" + "\n"+
            //    "**Rank:** Unranked\n"+
            //    "**Friendlies:** 15\n"+
            //    "**Ranked:** 0"));
            //builder.AddField(new EmbedFieldBuilder().WithName("Super Smash Bros Wii U").WithValue(
            //    "**Mains:** Ganondorf, Pikachu\n" +
            //    "**Ranked:** Unranked\n" +
            //    "**Friendlies:** 0\n" +
            //    "**Ranked:** 0"));
            //builder.WithUrl(user.profile_url);


            //var list = new List<Match>();
            //list.Add(new Match("Friendlies", "Melee", "BortTheBeaver", "Benzouz", null));
            //list.Add(new Match("Friendlies", "Melee", "BortTheBeaver", "faked", "faked"));
            //list.Add(new Match("Friendlies", "Melee", "BortTheBeaver", "sandwichyo", null));

            //string matches = "";
            //foreach (var match in list)
            //{
            //    if(match.Winner == null)
            //    matches += match.Game + " " + match.Type + ": " + match.Player1 + " vs " + match.Player2;
            //    else if (match.Winner == match.Player1)
            //        matches += match.Game + " " + match.Type + ": **" + match.Player1 + "** vs " + match.Player2;
            //    else matches += match.Game + " " + match.Type + ": " + match.Player1 + " vs **" + match.Player2 + "**";
            //    matches += "\n";
            //}
            //builder.AddField(new EmbedFieldBuilder().WithName("Match History (First 5)").WithValue(
            //    matches));
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
        public int id { get; set; }
    }

    public class Stats
    {
        public int ranked_played { get; set; }
        public int total_friendlies_played { get; set; }
        public int unique_opponents { get; set; }
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
        public object name { get; set; }
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

    public class __invalid_type__2
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
    [JsonProperty(PropertyName = "2")]
    public __invalid_type__2 __invalid_name__2 { get; set; }
    [JsonProperty(PropertyName = "4")]
    public __invalid_type__4 __invalid_name__4 { get; set; }
}

public class MemberSince
{
    public int timestamp { get; set; }
    public string courtesy { get; set; }
    public string full { get; set; }
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
    public object now_playing { get; set; }
}
}

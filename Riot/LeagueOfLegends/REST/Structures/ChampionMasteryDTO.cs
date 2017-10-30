using System;
using RestSharp;
using Newtonsoft.Json;

namespace Riot.LeagueOfLegends.REST.Structures
{
    public struct ChampionMasteryDTO
    {
        private struct ChampionMasteryInformation
        {
            /// <summary>
            /// Is chest granted for this champion or not in current season.
            /// </summary>
            [JsonProperty("chestGranted")]
            bool Granted { get; }
            /// <summary>
            /// Champion level for specified player and champion combination.
            /// </summary>
            [JsonProperty("championLevel")]
            int Level { get; }
            /// <summary>
            /// Total number of champion points for this player and champion combination - they are used to determine championLevel.
            /// </summary>
            [JsonProperty("championPoints")]
            int Points { get; }
            /// <summary>
            /// Champion ID for this entry.
            /// </summary>
            [JsonProperty("championId")]
            long Id { get; }
            /// <summary>
            /// Number of points needed to achieve next level. Zero if player reached maximum champion level for this champion.
            /// </summary>
            [JsonProperty("championPointsUntilNextLevel")]
            long PointsUntilNextLevel { get; }
            /// <summary>
            /// Number of points earned since current level has been achieved. Zero if player reached maximum champion level for this champion.
            /// </summary>
            [JsonProperty("championPointsSinceLastLevel")]
            long PointsSinceLastLevel { get; }
            /// <summary>
            /// Last time this champion was played by this player - in Unix milliseconds time format.
            /// </summary>
            [JsonProperty("lastPlayTime")]
            private long _lastPlayTime { get; }
            /// <summary>
            /// Last time this champion was played by this player - in DateTime time format.
            /// </summary>
            DateTime LastPlayTime { get; }
            /// <summary>
            /// The number of tokens received for this champion.
            /// </summary>
            [JsonProperty("tokenEarned")]
            int TokenEarned { get; }

            public ChampionMasteryInformation(string content)
            {
                this = JsonConvert.DeserializeObject<ChampionMasteryInformation>(content);
                this.LastPlayTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(this._lastPlayTime);
            }
        }

        private struct SummonerInformation
        {
            /// <summary>
            /// Summoner's Id.
            /// </summary>
            [JsonProperty("playerId")]
            long Id { get; }

            public SummonerInformation(string content)
            {
                this = JsonConvert.DeserializeObject<SummonerInformation>(content);
            }
        }

        /// <summary>
        /// Champion data
        /// </summary>
        ChampionMasteryInformation Champion { get; }
        /// <summary>
        /// Summoner data
        /// </summary>
        SummonerInformation Summoner { get; }

        public ChampionMasteryDTO(RestResponse response)
        {
            this.Champion = new ChampionMasteryInformation(response.Content);
            this.Summoner = new SummonerInformation(response.Content);
        }
    }
}

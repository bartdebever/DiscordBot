using System.Threading.Tasks;
using RestSharp;
using Riot.Attributes;
using Riot.Enums;
using Riot.LeagueOfLegends.REST.Interfaces;
using Riot.LeagueOfLegends.REST.Structures;
using Riot.Services.Interfaces;
using Riot.Services;

namespace Riot.LeagueOfLegends.REST
{
    /// <summary>
    /// Champion-Mastery
    /// https://developer.riotgames.com/api-methods/#champion-mastery-v3
    /// </summary>
    [Group("lol", "champion-mastery")]
    public class ChampionMastery : IChampionMastery, IService, IEndpoint
    {
        private readonly RestClient client;

        public ChampionMastery(RestClient client)
        {
            this.client = client;
        }

        public ChampionMasteryDTO[] GetAllChampionMasteries(ServiceRegion region, )
    }
}

using System.Threading.Tasks;
using Riot.Enums;
using Riot.LeagueOfLegends.REST.Structures;

namespace Riot.LeagueOfLegends.REST.Interfaces
{
    public interface IChampionMastery
    {
        ChampionMasteryDTO[] GetAllChampionMasteries(ServiceRegion region, long summonerId);
        Task<ChampionMasteryDTO[]> GetAllChampionMasteriesAsync(ServiceRegion region, long summonerId);
    }
}

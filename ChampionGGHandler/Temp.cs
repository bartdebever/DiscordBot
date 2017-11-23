using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using DataLibrary;

namespace ChampionGGHandler
{
    public static class Temp
    {
        public static string GetChampionName(int id)
        {
            return new RiotData().Champions.FirstOrDefault(x => x.ChampionId == id).name;
        }

        public static void GetChammpions()
        {
            var client = new RestClient(new Uri("https://euw1.api.riotgames.com"));
            var request =
                new RestRequest(
                    "lol/static-data/v3/champions?locale=en_US&dataById=false&api_key=");
            var response = client.Execute(request);
            var json = response.Content;
            var champions = JsonConvert.DeserializeObject<RootChampion>(json).data;
            var database = new RiotData();
            foreach (var champ in champions.AllChampions)
            {
                database.Champions.Add(champ);
            }
            database.SaveChanges();

        }

    }
}

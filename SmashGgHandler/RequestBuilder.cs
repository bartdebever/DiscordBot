using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SmashGgHandler
{
    public static class RequestBuilder
    {
        public static string GetTournamentInfoJson(string tournament)
        {
            var client = new RestClient(Adresses.BaseUri);
            var request = new RestRequest($"{Adresses.TournamentEndpoint}{tournament}?{Adresses.Event}&expand[]=groups&expand[]=phase", Method.GET);
            return client.Execute(request).Content;
        }

        public static string GetTournamentResultJson(int groupId)
        {
            var client = new RestClient(Adresses.BaseUri);
            var request = new RestRequest($"phase_group/{groupId}?expand[]=entrants&expand[]=seeds", Method.GET);
            return client.Execute(request).Content;
        }
    }
}

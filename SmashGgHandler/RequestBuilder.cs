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
            var request = new RestRequest($"{Adresses.TournamentEndpoint}{tournament}?{Adresses.Event}", Method.GET);
            return client.Execute(request).Content;
        }
    }
}

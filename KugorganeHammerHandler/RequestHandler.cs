using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace KugorganeHammerHandler
{
    public static class RequestHandler
    {
        public static Character GetCharacterName(string name)
        {
            string json = RequestBuilder.GetChampion(name);
            return JsonConvert.DeserializeObject<Character>(json);
        }
    }

    public static class RequestBuilder
    {
        public static Uri BaseUri => new Uri("http://beta-api-kuroganehammer.azurewebsites.net/api");
        public static string CharacterEndpoint => "characters/";

        public static string GetChampion(string name)
        {
            var client = new RestClient(BaseUri);
            var request = new RestRequest(CharacterEndpoint + "name/" + name, Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}

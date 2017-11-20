using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmashHandler.Constant_data;
using SmashHandler.DataTypes;

namespace SmashHandler
{
    public static class RequestHandler
    {
        public static RootObject GetUserByName(string username)
        {
            username = username.Replace(" ", "-");
            HttpWebRequest request = RequestBuilder.GetRequest(Adresses.BaseUri+Endpoints.MatchmakingEndpoint+"?username="+username);
            string result = null;
            try
            {
                using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch
            {
                
            }
            RootObject root = JsonConvert.DeserializeObject<RootObject>(result);
            return root;
        }
    }
}

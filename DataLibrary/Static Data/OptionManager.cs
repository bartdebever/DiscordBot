using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataLibrary.Static_Data
{
    public static class OptionManager
    {
        private static string discordAPIKey = "";
        private static string riotAPIKey = "";
        private static string smashHash = "";
        private static bool initialized = false;
        public static string DiscordKey
        {
            get
            {
                if (!initialized)
                {
                    Initialize();
                }
                return discordAPIKey;
            }
        }

        public static string RiotKey
        {
            get
            {
                if (!initialized)
                {
                    Initialize();
                }
                return riotAPIKey;
            }
        }

        public static string SmashHash
        {
            get
            {
                if (!initialized)
                {
                    Initialize();
                }
                return smashHash;
            }
        }

        public static void Initialize()
        {
            using (StreamReader file = new StreamReader(@"C:\Users\Bort\source\repos\DiscordBot\DataLibrary\Configuration.json")) 
            //TODO Make dynamic
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject ob = (JObject)JToken.ReadFrom(reader);
                discordAPIKey = ob.GetValue("DiscordAPI").ToString();
                riotAPIKey = ob.GetValue("RiotAPI").ToString();
                smashHash = ob.GetValue("SmashHash").ToString();
            }
            initialized = true;
        }
    }
}

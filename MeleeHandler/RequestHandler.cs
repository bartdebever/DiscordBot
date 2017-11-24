using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeleeHandler
{
    public static class RequestHandler
    {
        public static MeleeCharacter GetCharacter(int id)
        {
            var json = RequestBuilder.GetCharacter(id);
            var chararcter = JsonConvert.DeserializeObject<MeleeCharacter>(json);
            return chararcter;
        }
    }
}

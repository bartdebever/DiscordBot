using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Discord.Implemented;
using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Static_Data
{
    public static class DatabaseManager
    {
        private static readonly Mock Mock = new Mock();

        public static Mock GetMock()
        {
            return Mock;
        }

        public static int UserKeyGenerator()
        {
            int next = 0;
            lock (new object())
            {
                Random random = new Random(); //TODO Add random seed
                bool validint = false;
                while (!validint)
                {
                    DiscordUser user = null;
                    next = random.Next(1, int.MaxValue - 1);
                    user = Mock.Users.FirstOrDefault(x => x.Id == next);
                    if (user == null)
                    {
                        validint = true;
                    }
                }
            }
            return next;

        }

        public static string GenerateString()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

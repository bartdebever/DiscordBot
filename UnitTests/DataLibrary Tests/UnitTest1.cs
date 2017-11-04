using System;
using System.Collections.Generic;
using DataLibrary.Discord.Implemented;
using DataLibrary.Useraccounts.Implementation;
using DataLibrary.Useraccounts.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.DataLibrary_Tests
{
    [TestClass]
    public class UserTests
    {
        private List<ISummoner> summoners;
        private IUser user;
        [TestInitialize]
        public void Initialize()
        {
            summoners = new List<ISummoner>();
            user = new DiscordUser(0, "Bort", summoners, DateTime.Now);
    }

        [TestMethod]
        public void FieldTest()
        {
            Assert.AreEqual("Bort", user.Name, "Name not saved correctly");
            Assert.AreEqual(0, user.Id, "Id not saved correctly");
            Assert.AreEqual(summoners, user.Summoners, "List not saved correctly");
            Assert.AreEqual(0, user.Summoners.Count, "Summoners list already contains items");
        }

        [TestMethod]
        public void AddSummoner()
        {
            ISummoner summoner = new APISummoner(0, "euw");
            Assert.AreEqual(0, user.Summoners.Count, "List already contains summoners");
            user.AddSummoner(summoner);
            Assert.AreEqual(1, user.Summoners.Count, "Summoners not properly added");
            Assert.AreEqual(0, user.Summoners[0].SummonerId, "Summoners Id incorrectly saved in list.");
            Assert.AreEqual("euw", user.Summoners[0].Region, "Region incorrectly saved in list.");
        }
    }
}

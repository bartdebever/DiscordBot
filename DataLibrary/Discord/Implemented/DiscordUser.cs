﻿using System;
using System.Collections.Generic;
using DataLibrary.Useraccounts.Implementation;
using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Discord.Implemented
{
    public class DiscordUser : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ISummoner> Summoners { get; set; }
        public SmashAccount SmashAccount { get; set; }
        public DateTime CreationDate { get; set; }

        public void AddSummoner(ISummoner summoner)
        {
            if (!Summoners.Contains(summoner))
            {
                Summoners.Add(summoner);
            }
        }

        /// <summary>
        /// The Id used by Discord to identify the user.
        /// </summary>
        public long Discordid { get; set; }

        public DiscordUser(int id, string name, List<ISummoner> list, DateTime creationDate, SmashAccount smashAccount)
        {
            this.Id = id;
            this.Name = name;
            this.Summoners = list;
            this.CreationDate = creationDate;
            this.SmashAccount = smashAccount;
        }

        public DiscordUser()
        {
            this.Summoners = new List<ISummoner>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using IRole = DataLibrary.Interfaces.IRole;

namespace DataLibrary.Discord
{
    /// <summary>
    /// A class used to keep track of the roles that a server has within their Discord
    /// </summary>
    public class DiscordRole : IRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// The Id which the role is identified
        /// </summary>
        public long DiscordId { get; set; }
        /// <summary>
        /// The color that the role has in the discord format
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// A list of words that the role can be assigned by.
        /// Example: The role with the name "Bronze Noobs" can be assigned by using "Bronze".
        /// </summary>
        public List<string> Alias { get; set; }
        /// <summary>
        /// Add a word to the Alias list.
        /// Throws an exception if the word is already in the list.
        /// Word can not be null or an empty value.
        /// </summary>
        /// <param name="word">the word needing to be added</param>
        public void AddAlias(string word)
        {
            if (!Alias.Contains(word)) Alias.Add(word);
            //TODO Throw custom exception
        }
        /// <summary>
        /// Removes a word from the Alias list
        /// Throws an exception if the word is already in the list.
        /// </summary>
        /// <param name="word"></param>
        public void RemoveAlias(string word)
        {
            if (Alias.Contains(word)) Alias.Remove(word);
            //TODO Throw custom exception
        }
    }
}

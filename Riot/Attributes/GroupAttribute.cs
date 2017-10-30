using System;

namespace Riot.Attributes
{
    /// <summary>
    /// Attach game and API method to the method
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class GroupAttribute : Attribute
    {
        /// <summary>
        /// The game to target from Riot Games
        /// </summary>
        public readonly string Game;
        /// <summary>
        /// Group of methods as seen on https://developer.riotgames.com/api-methods/
        /// </summary>
        public readonly string Group;

        public GroupAttribute(string game, string group)
        {
            this.Game = game;
            this.Group = group;
        }
    }
}

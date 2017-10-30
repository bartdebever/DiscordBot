using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Interfaces
{
    /// <summary>
    /// IGame is used to keep track of some games within our system.
    /// This could be used to track owned games within our system or add them using the Discord "playing" feature.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// The name of the game the developers have given it.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// A simple description about the game
        /// </summary>
        string Description { get; set; }
    }
}

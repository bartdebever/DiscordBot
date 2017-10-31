using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Interfaces
{
    /// <summary>
    /// Used for collectable items such as currency or other profile items.
    /// </summary>
    public interface ICollectable
    {
        /// <summary>
        /// Internal Id given to the collectable unit
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Name given to the internal unit and displayed in shop
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Prince of the unit able to be bought in the shop.
        /// This will be -1 for the currency itself.
        /// </summary>
        double Price { get; set; }
        /// <summary>
        /// Amount of items that have been obtained.
        /// </summary>
        double Amount { get; set; }
        /// <summary>
        /// The amount of coins the user will get back for selling this item
        /// </summary>
        double SellPrice { get; set; }
        /// <summary>
        /// A description of the unit.
        /// What does the user get when buying this
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// The user in question buys the item.
        /// </summary>
        /// <param name="user">User buying the item</param>
        void Buy(IUser user);
        /// <summary>
        /// The user in question sells the item.
        /// </summary>
        /// <param name="user">User selling the item</param>
        void Sell(IUser user);
    }
}

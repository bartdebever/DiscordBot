using DataLibrary.Interfaces;
using DataLibrary.Shop.Exceptions;
using DataLibrary.Shop.Interfaces;
using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Shop.Implementation
{
    public class Coin : ICollectable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public double SellPrice { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// A coin is not allowed be be bought.
        /// </summary>
        /// <param name="user"></param>
        public void Buy(IUser user)
        { 
            throw new CoinBuyException();
        }
        /// <summary>
        /// A coin is not allowed to be sold.
        /// </summary>
        /// <param name="user"></param>
        public void Sell(IUser user)
        {
            throw new CoinSellException();
        }

        /// <summary>
        /// A coin doesn't have a price nor a sellprice, these are both set to -1.
        /// If a coin tries to be sold or bought it should throw an exception
        /// </summary>
        /// <param name="name">The name of the coin in question, could be hardcoded.</param>
        /// <param name="description">The description of the coin, could be hardcoded.</param>
        /// <param name="amount">The amount of coin the user has.</param>
        public Coin(string name, string description, double amount)
        {
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Price = -1;
            this.SellPrice = -1;
            this.Id = 0;
        }
    }
}

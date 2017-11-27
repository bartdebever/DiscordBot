using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Shop
{
    public class ShopItem
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime ValidUntill { get; set; }
        public string ImageUrl { get; set; }
        public string Condition { get; set; }
        public ItemType Type { get; set; }
        public bool Buyable { get; set; }
        public string Game { get; set; }
        public string Series { get; set; }
        public double Discount { get; set; }
        public DateTime OnSaleUntill { get; set; }
        public ShopItem() { }

        public ShopItem(int id, double prince, string name, string description, string code, string imageUrl,
            string condition, ItemType type, bool buyable, string game, string series)
        {
            this.Id = id;
            this.Price = prince;
            this.Name = name;
            this.Description = description;
            this.Code = code;
            this.ImageUrl = imageUrl;
            this.Condition = condition;
            this.Type = type;
            this.Buyable = buyable;
            this.Game = game;
            this.Series = series;
        }
    }

    public enum ItemType
    {
        Badge,
        Achievement,
        Icon,
        Border,
        Background
    }
}

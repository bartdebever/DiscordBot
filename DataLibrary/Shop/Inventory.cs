using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Shop.Interfaces;

namespace DataLibrary.Shop
{
    public class Inventory
    {
        public int Id { get; set; }
        public double Currency { get; set; }
        public List<ShopItem> Items { get; set; }
    }
}

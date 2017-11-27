using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Shop
{
    public static class ShopItems
    {
        private static List<ShopItem> _shopItems = new List<ShopItem>()
        {
            new ShopItem(1, 0, "Season 2017 Diamond", "", "", "", "Reach rank Diamond in Melee on Anther's Ladder to earn the Season 2017 Diamond Badge", ItemType.Badge, false,"Super Smash Bros Melee", "SmashLadder 2017 Ranked"),
            new ShopItem(2, 30, "Metal Mewtwo Icon", "A Metal Mewtwo Icon as a profile icon made by *Person*", "MewtwoForMeleeHD", "https://pre00.deviantart.net/443f/th/pre/i/2014/290/0/3/armored_mewtwo_by_tomycase-d83600c.png", "", ItemType.Icon, true, "Super Smash Bros 4", "Smash Bros Icons"),
            new ShopItem(3, 1000, "The Big Spender Icon", "Show off how much money you have!","","","", ItemType.Icon, true, "", "Big Spender Icons"),
            new ShopItem(4, 300, "Winter 2017 Border", "All the snowy goodness in one border","","","", ItemType.Border, true, "", "Winter 2017"),
            new ShopItem(5, 100, "Zoe Background", "Show off the little summoner stealer as your background", "","http://ddragon.leagueoflegends.com/cdn/img/champion/splash/Zoe_1.jpg","", ItemType.Background, true, "League of Legends", "League of Legends Champion")
        };

        public static List<ShopItem> GetShopItems => _shopItems;
    }
}

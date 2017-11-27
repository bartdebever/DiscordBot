using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Shop;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;

namespace DiscordBot.Modules
{
    [Group("Shop")]
    public class ShopModule: ModuleBase
    {
        [Command("list")]
        public async Task GetListActiveItems()
        {
            var items = ShopItems.GetShopItems.Where(x=> x.Buyable);
            var builder = Builders.BaseBuilder("Shop items currently in stock",
                "Displaying this season\'s buyable items", Color.DarkOrange, null, "");
            foreach (var type in Enum.GetValues(typeof(ItemType)))
            {
                if (type.ToString() != ItemType.Achievement.ToString())
                {
                    string details = "";
                    var itemsFromType = items.Where(x => Equals(x.Type, type) && x.Buyable).ToList();
                    if (itemsFromType.Count > 0)
                        itemsFromType.ForEach(x =>
                            details += $"**{x.Name}: ** {x.Description} **\n  Cost: **{x.Price} Currency\n");
                    else details = $"No {type} available for sale right now.";
                    builder.AddField(type.ToString(), details);
                }

            }
            await ReplyAsync("", embed: builder.Build());
        }

        [Command("item")]
        public async Task ItemInfo([Remainder] string itemname)
        {
            Discord.EmbedBuilder builder = null;
            var item = ShopItems.GetShopItems.Where(x => x.Name.ToLower().Contains(itemname.ToLower())).ToList();
            if (item.Count > 1)
            {
                builder = Builders.BaseBuilder("Multiple items found", "", Color.Red, null,"");
                string items = "";
                item.ForEach(x=> items += x.Name + "\n");
                builder.AddField("Items found:", items);
            }
            else
            {
                var shopitem = item[0];
                builder = Builders.BaseBuilder(shopitem.Name, shopitem.Description, Color.DarkPurple, null, "");
                string details = "";
                if (shopitem.Price != 0) details+= $"**Price: **{shopitem.Price}\n";
                if (!string.IsNullOrEmpty(shopitem.Condition)) details += $"**Condition: **{shopitem.Condition}\n";
                if (!string.IsNullOrEmpty(shopitem.Game)) details += $"**Game: **{shopitem.Game}\n";
                if (!string.IsNullOrEmpty(shopitem.Series)) details += $"**Series: **{shopitem.Series}\n";
                if (!string.IsNullOrEmpty(shopitem.ImageUrl)) builder.ImageUrl = shopitem.ImageUrl;
                details += $"**Type: **{shopitem.Type}";
                builder.AddField("Details:", details);
            }
            await ReplyAsync("", embed: builder.Build());
        }

        [Command("Redeem")]
        public async Task Redeem([Remainder] string code)
        {
            Discord.EmbedBuilder builder = null;
            var item = ShopItems.GetShopItems.FirstOrDefault(x => x.Code == code && DateTime.Now < x.ValidUntill);
            if (item == null)
            {
                
            }
        }
    }
}

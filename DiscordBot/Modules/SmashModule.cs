using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;
using KugorganeHammerHandler;

namespace DiscordBot.Modules
{
    [Group("Smash")]
    public class SmashModule : ModuleBase
    {
        [Group("WiiU")]
        public class WiiU : ModuleBase
        {
            [Command("")]
            public async Task GetCharacter([Remainder] string name)
            {
                var character = RequestHandler.GetCharacterName(name);
                Discord.EmbedBuilder builder = Builders.BaseBuilder("", "", Color.DarkTeal,
                    new EmbedAuthorBuilder().WithName("KuroganeHammer Result:").WithUrl("http://kuroganehammer.com"),
                    character.ThumbnailURL);
                builder.WithImageUrl(character.MainImageURL);
                builder.WithUrl(character.FullURL);
                string info = "";
                info += "**Name: **" + character.Name;
                if (!string.IsNullOrEmpty(character.Description))
                {
                    info += "**Description: **" + character.Description;
                }
                if (!string.IsNullOrEmpty(character.Style)) info += "**Style: **" + character.Style;
                builder.AddField(new EmbedFieldBuilder().WithName("Information").WithValue(info));
                var movement = RequestHandler.GetMovement(name);
                var half = movement.Attributes.Count / 2;
                var info1 = "";
                var info2 = "";
                for (int i = 0; i < movement.Attributes.Count / 2; i++)
                {
                    info1 += $"**{movement.Attributes[i].Name}**: {movement.Attributes[i].Value}\n";
                    info2 += $"**{movement.Attributes[i+half].Name}**: {movement.Attributes[i+half].Value}\n";
                }
                builder.AddInlineField("Movement", info1);
                builder.AddInlineField("Information", info2);
                await ReplyAsync("", embed: builder.Build());
            }
        }

        [Group("Melee")]
        public class Melee : ModuleBase
        {
            [Command("")]
            public async Task GetCharacter([Remainder] string name)
            {
                var characters = new List<string>()
                {
                    "Bowser",
                    "Captain Falcon",
                    "Donkey Kong",
                    "Dr. Mario",
                    "Falco",
                    "Fox",
                    "Ganondorf",
                    "Ice Climbers",
                    "Jugglypuff",
                    "Kirby",
                    "Link",
                    "Luigi",
                    "Mario",
                    "Marth",
                    "Mewtwo",
                    "Mr. Game And Watch",
                    "Ness",
                    "Peach",
                    "Pichu",
                    "Pikachu",
                    "Roy",
                    "Samus",
                    "Sheik",
                    "Yoshi",
                    "Young Link",
                    "Zelda"
                };
                var id = characters.IndexOf(name) + 1;
                var character = MeleeHandler.RequestHandler.GetCharacter(id);
                var builder = Builders.BaseBuilder("", "", Color.Teal,
                    new EmbedAuthorBuilder().WithName(character.name).WithIconUrl($"http://smashlounge.com/img/pixel/{character.name.Replace(" ","")}HeadSSBM.png"), "");
                builder.AddField("Description", character.guide);
                builder.AddField("Stats", $"**Tier: **{character.tierdata}\n" +
                                          $"**Weight: **{character.weight}\n" +
                                          $"**Fallspeed: **{character.fallspeed}\n" +
                                          $"**Can Walljump: **{Convert.ToBoolean(Int32.Parse(character.walljump))}");
                await ReplyAsync("", embed: builder.Build());
            }
        }
    }
}

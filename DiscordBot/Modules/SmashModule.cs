using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.EmbedBuilder;
using DiscordBot.Loggers;
using KugorganeHammerHandler;
using KugorganeHammerHandler.Data_Types;

namespace DiscordBot.Modules
{
    [Group("Smash")]
    public class SmashModule : ModuleBase
    {
        [Group("WiiU")]
        public class WiiU : ModuleBase
        {
            [Command("character")]
            public async Task GetCharacter([Remainder] string name)
            {
                var character = RequestHandler.GetCharacterName(name);
                var moves = RequestHandler.GetMoves(name);
                Discord.EmbedBuilder builder = Builders.BaseBuilder("", "", Color.DarkTeal,
                    new EmbedAuthorBuilder().WithName("KuroganeHammer Result:").WithUrl("http://kuroganehammer.com"),
                    character.ThumbnailURL);
                //builder.WithImageUrl(character.MainImageURL);
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
                builder.AddInlineField("Movement", info2);
                string movesinfo = "";
                string specials = "";
                int specialcount = 0;
                string aerials = "";
                string smashes = "";
                string grabs = "";
                string tilts = "";
                moves.ForEach(x =>
                {
                    if (x.MoveType == "ground")
                    {
                        if (x.Name.Contains("smash"))
                        {
                            smashes += x.Name + "\n";
                        }
                        else if (x.Name.Contains("tilt"))
                        {
                            tilts += x.Name + "\n";
                        }
                        else if (x.Name.ToLower().Contains("grab"))
                        {
                            grabs += x.Name + "\n";
                        }
                        else
                        {
                            movesinfo += x.Name + "\n";
                        }
                    }

                    if (x.MoveType == "special")
                    {
                            specials += x.Name + "\n";
                            specialcount++;
                        
                    }
                    if (x.MoveType == "aerial") aerials += x.Name + "\n";
                });
                builder.AddInlineField("Ground Moves", movesinfo);
                builder.AddInlineField("Smashes", smashes);
                builder.AddInlineField("Specials", specials);
                builder.AddInlineField("Aerials", aerials);
                builder.AddInlineField("Tilts", tilts);
                builder.AddInlineField("Grabs", tilts);
                await ReplyAsync("", embed: builder.Build());
            }

            [Command("Move")]
            public async Task GetMove(string charactername, [Remainder] string moveName)
            {
                Discord.EmbedBuilder builder = null;
                var character = RequestHandler.GetCharacterName(charactername);
                var moves = RequestHandler.GetMoves(charactername);
                Move move = null;
                move = moves.FirstOrDefault(x=> x.Name.ToLower().Equals(moveName.ToLower()));
                if (move == null) move = moves.FirstOrDefault(x => x.Name.ToLower().Contains(moveName.ToLower()));
                if (move != null)
                {
                    builder = Builders.BaseBuilder("", "", Color.DarkBlue, new EmbedAuthorBuilder().WithName(character.Name +  " | " +move.Name).WithUrl("http://kuroganehammer.com/smash4/"+move.Owner), character.ThumbnailURL);
                    string statistics = "";
                    //builder.WithImageUrl("https://zippy.gfycat.com/EuphoricCookedHornshark.webm"); //TODO Add later when gifs are supported but holy shit it works.
                    if (!string.IsNullOrEmpty(move.MoveType)) statistics += "**Move Type:** " + move.MoveType + "\n";
                    if (!string.IsNullOrEmpty(move.BaseDamage)) statistics += "**Base Damage:** " + move.BaseDamage + "%\n";
                    if (!string.IsNullOrEmpty(move.BaseKockbackSetKnockback)) statistics += "**Base/Set Knockback: **" + move.BaseKockbackSetKnockback + "\n";
                    if (!string.IsNullOrEmpty(move.LandingLag)) statistics += "**Landinglag: **" + move.LandingLag + " frames\n";
                    if (!string.IsNullOrEmpty(move.HitboxActive)) statistics += "**Hitbox Active: **Frames " + move.HitboxActive + "\n";
                    if (!string.IsNullOrEmpty(move.KnockbackGrowth)) statistics += "**Knockback Growth: **" + move.HitboxActive + "\n";
                    if (!string.IsNullOrEmpty(move.Angle)) statistics += "**Angle: **" + move.Angle.Replace("361", "Sakuari Angle/361") + "\n";
                    if (!string.IsNullOrEmpty(move.AutoCancel)) statistics += "**Auto-Cancel: **Frame " + move.AutoCancel.Replace("&gt;", ">") + "\n";
                    if (!string.IsNullOrEmpty(move.FirstActionableFrame)) statistics += "**First Actionable Frame: **" + move.FirstActionableFrame + "\n";
                    builder.AddInlineField("Statistics",
                    statistics);
                    if (move.Angle.Equals("361"))
                    {
                        builder.AddInlineField("Sakurai Angle",
                            "\"The Sakurai angle (sometimes displayed as * in moveset lists) is a special knockback behavior that many attacks use. While it reads in the game data as an angle of 361 degrees, the actual resulting angle is dependent on whether the victim is on the ground or in the air, as well as the strength of the knockback.\"\nSource: https://www.ssbwiki.com/Sakurai_angle");
                    }
                    
                    builder.AddField("Info",
                        "If you don't understand the values please visit http://kuroganehammer.com/Glossary");
                }
                await ReplyAsync("", embed: builder.Build());
            }
        }

        [Group("Melee")]
        public class Melee : ModuleBase
        {
            [Command("character")]
            public async Task GetCharacter([Remainder] string name)
            {
                var character = MeleeHandler.RequestHandler.GetCharacter(name);
                var builder = Builders.BaseBuilder("", "", Color.Teal,
                    new EmbedAuthorBuilder().WithName(character.name).WithIconUrl($"http://smashlounge.com/img/pixel/{character.name.Replace(" ","")}HeadSSBM.png"), "");
                builder.AddField("Description", character.guide);
                builder.AddField("Stats", $"**Tier: **{character.tierdata}\n" +
                                          $"**Weight: **{character.weight}\n" +
                                          $"**Fallspeed: **{character.fallspeed}\n" +
                                          $"**Can Walljump: **{Convert.ToBoolean(Int32.Parse(character.walljump))}");
                if (character.gifs != null)
                {
                    foreach (var smashLoungeGif in character.gifs)
                    {
                        builder.AddInlineField(smashLoungeGif.Description,
                            $"**Link: **https://zippy.gfycat.com/{smashLoungeGif.Url}.webm \n" +
                            $"**Source: **{smashLoungeGif.Source}\n");
                    }
                }
                await ReplyAsync("", embed: builder.Build());
            }

            [Command("tech")]
            public async Task GetTech([Remainder] string name)
            {
                var tech = MeleeHandler.RequestHandler.GetTechnique(name);
                var builder = Builders.BaseBuilder("", "", Color.Teal, new EmbedAuthorBuilder().WithName(tech.TechName),
                    "");
                builder.AddField("Information",
                    $"**Description: **{tech.Description}\n" +
                    $"**Inputs: **{tech.Inputs}\n" +
                    $"**SmashWiki Link: **{tech.SmashWikiLink}");
                if (tech.Gifs != null)
                {
                    builder.WithImageUrl($"https://zippy.gfycat.com/{tech.Gifs[0].Url}.gif");
                    foreach (var gif in tech.Gifs)
                    {
                        string source = "";
                        if (!string.IsNullOrEmpty(gif.Source)) source = $"**Source: **{gif.Source}";
                        builder.AddInlineField(gif.Description,
                            $"**Link: **https://zippy.gfycat.com/{gif.Url}.webm \n" +source
                            );
                    }
                }
                await ReplyAsync("", embed: builder.Build());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampionGGHandler;
using ChampionGGHandler.DataTypes;
using Discord;

namespace DiscordBot.EmbedBuilder
{
    public static class ChampionGGBuilder
    {
        public static Discord.EmbedBuilder GetChampionInfo(ChampionData champion)
        {
            var builder = Builders.BaseBuilder("", "", Color.Magenta,
                new EmbedAuthorBuilder().WithName("Champion.gg information").WithUrl("http://champion.gg"), "");
            builder.AddField("Champion Information",
                $"**Champion: **{Temp.GetChampionName(champion.ChampionId)}\n" +
                $"**Role: **{champion.Role}\n" +
                $"**Patch: **{champion.Patch}");
            builder.AddField("Overall Statistics:",
                $"**WinRate: **{Math.Round(champion.WinRate*100, 2)}%\n" +
                $"**PlayRate: **{Math.Round(champion.PlayRate*100, 2)}%\n" +
                $"**Role Played Percentage: **{Math.Round(champion.PercentageRolePlayed*100, 2)}%\n" +
                $"**Total Games Played: **{champion.TotalGamesPlayed}");
            builder.AddField("Position Placings:",
                $"**Kills: ** {champion.PositionData.Kills}th\n" +
                $"**Deaths: **{champion.PositionData.Deaths}th\n" +
                $"**Assists: **{champion.PositionData.Assists}th\n" +
                $"**Gold Earned: **{champion.PositionData.GoldEarned}th\n" +
                $"**WinRate: **{champion.PositionData.WinRates}th\n" +
                $"**BanRate: **{champion.PositionData.BanRates}th\n" +
                $"**PickRate: **{champion.PositionData.PlayRates}th");
            builder.AddField("Average Statistics:",
                $"**Kills: **{Math.Round(champion.AverageKills, 3)}\n" +
                $"**Deaths: **{Math.Round(champion.AverageDeaths, 3)}\n" +
                $"**Assists: **{Math.Round(champion.AverageAssists, 3)}\n" +
                $"**Minions killed:** {Math.Round(champion.MinionsKilled, 3)}\n" +
                $"**Allied Jungle Monsters Killed: **{Math.Round(champion.MonstersKilledTeamJungle, 3)}\n" +
                $"**Enemy Jungle Monsters Killed: **{Math.Round(champion.MonstersKilledEnemyJungle, 3)}\n" +
                $"**Damage Healed: **{Math.Round(champion.Healed, 3)}\n");
            return builder;
        }

        public static EmbedFieldBuilder GetFieldByLane(string lane, PositionStat stats)
        {
            return new EmbedFieldBuilder().WithName(lane).WithValue(
                $"**Highest Winrate: **{Temp.GetChampionName(stats.winrate.best.championId)} {Math.Round(stats.winrate.best.score*100,2)}%" +
                $"\n**Lowest Winrate: **{Temp.GetChampionName(stats.winrate.worst.championId)} {Math.Round(stats.winrate.worst.score*100,2)}%" +
                $"\n**Best Performance: **{Temp.GetChampionName(stats.performanceScore.best.championId)} {stats.performanceScore.best.score}\n" +
                $"**Worst Performance: **{Temp.GetChampionName(stats.performanceScore.worst.championId)} {stats.performanceScore.worst.score}");
        }
    }
}

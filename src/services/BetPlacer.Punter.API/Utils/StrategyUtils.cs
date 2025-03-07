﻿using BetPlacer.Punter.API.Models.ValueObjects.Match;

namespace BetPlacer.Punter.API.Utils
{
    public static class StrategyUtils
    {
        public static List<string> GetStrategyNames()
        {
            List<string> strategiesName =
            [
                "PUNTER - Back Casa",
                "PUNTER - Back Empate",
                "PUNTER - Back Fora",
                "PUNTER - Over 2.5",
                "PUNTER - Under 2.5",
                "PUNTER - BTTS Sim",
                "PUNTER - BTTS Não",
                "PUNTER - Handicap +1.0 Casa",
                "PUNTER - Handicap +0.75 Casa",
                "PUNTER - Handicap +0.5 Casa",
                "PUNTER - Handicap +0.25 Casa",
                "PUNTER - Handicap 0.0 Casa",
                "PUNTER - Handicap -0.25 Casa",
                //"PUNTER - Handicap -0.5 Casa",
                "PUNTER - Handicap -0.75 Casa",
                "PUNTER - Handicap -1.0 Casa",
                "PUNTER - Handicap +1.0 Fora",
                "PUNTER - Handicap +0.75 Fora",
                "PUNTER - Handicap +0.5 Fora",
                "PUNTER - Handicap +0.25 Fora",
                "PUNTER - Handicap 0.0 Fora",
                "PUNTER - Handicap -0.25 Fora",
                //"PUNTER - Handicap -0.5 Fora",
                "PUNTER - Handicap -0.75 Fora",
                "PUNTER - Handicap -1.0 Fora",
            ];

            return strategiesName;
        }

        public static List<string> GetPriorityRankingGroupByStrategy(string strategyName)
        {
            List<string> ranking = new List<string>();

            switch (strategyName)
            {
                case "PUNTER - Back Casa":
                case "PUNTER - Back Empate":
                case "PUNTER - Back Fora":
                case "PUNTER - Handicap +1.0 Casa":
                case "PUNTER - Handicap +0.75 Casa":
                case "PUNTER - Handicap +0.5 Casa":
                case "PUNTER - Handicap +0.25 Casa":
                case "PUNTER - Handicap 0.0 Casa":
                case "PUNTER - Handicap -0.25 Casa":
                case "PUNTER - Handicap -0.5 Casa":
                case "PUNTER - Handicap -0.75 Casa":
                case "PUNTER - Handicap -1.0 Casa":
                case "PUNTER - Handicap +1.0 Fora":
                case "PUNTER - Handicap +0.75 Fora":
                case "PUNTER - Handicap +0.5 Fora":
                case "PUNTER - Handicap +0.25 Fora":
                case "PUNTER - Handicap 0.0 Fora":
                case "PUNTER - Handicap -0.25 Fora":
                case "PUNTER - Handicap -0.5 Fora":
                case "PUNTER - Handicap -0.75 Fora":
                case "PUNTER - Handicap -1.0 Fora":
                    ranking.Add("Grupo 1");
                    ranking.Add("Grupo 2");
                    ranking.Add("Grupo 3");
                    ranking.Add("Grupo 4");

                    break;
                case "PUNTER - Over 2.5":
                case "PUNTER - Under 2.5":
                    ranking.Add("Grupo 3");
                    ranking.Add("Grupo 4");
                    ranking.Add("Grupo 1");
                    ranking.Add("Grupo 2");

                    break;
                case "PUNTER - BTTS Sim":
                case "PUNTER - BTTS Não":
                    ranking.Add("Grupo 4");
                    ranking.Add("Grupo 3");
                    ranking.Add("Grupo 1");
                    ranking.Add("Grupo 2");

                    break;
                default:
                    break;
            }

            return ranking;
        }

        public static Dictionary<string, List<double>> GetMatchResults(List<MatchAnalyzed> matches, string strategy, double stake)
        {
            Dictionary<string, List<double>> results = new Dictionary<string, List<double>>();

            foreach (MatchAnalyzed match in matches)
            {
                double result = GetMatchResult(match, strategy, stake);

                if (results.ContainsKey(match.Season))
                    results[match.Season].Add(result);
                else
                    results.Add(match.Season, new List<double>() { result });
            }

            return results;
        }

        public static double GetMatchResult(MatchAnalyzed match, string strategy, double stake)
        {
            double result = 0;

            switch (strategy)
            {
                case "PUNTER - Back Casa":
                    if (match.HomeGoals > match.AwayGoals)
                        result = stake * match.HomeOdd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Back Empate":
                    if (match.HomeGoals == match.AwayGoals)
                        result = stake * match.DrawOdd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Back Fora":
                    if (match.HomeGoals < match.AwayGoals)
                        result = (stake * match.AwayOdd) - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Over 2.5":
                    if (match.HomeGoals + match.AwayGoals > 2)
                        result = stake * match.Over25Odd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Under 2.5":
                    if (match.HomeGoals + match.AwayGoals < 3)
                        result = stake * match.Under25Odd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - BTTS Sim":
                    if (match.HomeGoals > 0 && match.AwayGoals > 0)
                        result = stake * match.BttsYesOdd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - BTTS Não":
                    if (match.HomeGoals == 0 || match.AwayGoals == 0)
                        result = stake * match.BttsNoOdd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap +1.0 Casa":
                    if (match.HomeGoals >= match.AwayGoals)
                        result = stake * match.HomeHandicap1Odd - stake;
                    else if (match.HomeGoals - match.AwayGoals == -1)
                        result = 0;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap +0.75 Casa":
                    if (match.HomeGoals >= match.AwayGoals)
                        result = stake * match.HomeHandicap075Odd - stake;
                    else if (match.HomeGoals - match.AwayGoals == -1)
                        result = stake * 0.5 * -1;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap +0.5 Casa":
                    if (match.HomeGoals >= match.AwayGoals)
                        result = stake * match.HomeHandicap05Odd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap +0.25 Casa":
                    if (match.HomeGoals > match.AwayGoals)
                        result = stake * match.HomeHandicap025Odd - stake;
                    else if (match.HomeGoals == match.AwayGoals)
                        result = (stake * match.HomeHandicap025Odd - stake) * 0.5;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap 0.0 Casa":
                    if (match.HomeGoals > match.AwayGoals)
                        result = stake * match.HomeHandicap0Odd - stake;
                    else if (match.HomeGoals == match.AwayGoals)
                        result = 0;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap -0.25 Casa":
                    if (match.HomeGoals > match.AwayGoals)
                        result = stake * match.HomeHandicap025NegativeOdd - stake;
                    else if (match.HomeGoals == match.AwayGoals)
                        result = stake * 0.5 * -1;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap -0.5 Casa":
                    if (match.HomeGoals > match.AwayGoals)
                        result = stake * match.HomeHandicap05NegativeOdd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap -0.75 Casa":
                    if (match.HomeGoals > match.AwayGoals + 1)
                        result = stake * match.HomeHandicap075NegativeOdd - stake;
                    else if (match.HomeGoals == match.AwayGoals + 1)
                        result = (stake * match.HomeHandicap075NegativeOdd - stake) * 0.5;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap -1.0 Casa":
                    if (match.HomeGoals > match.AwayGoals + 1)
                        result = stake * match.HomeHandicap1NegativeOdd - stake;
                    else if (match.HomeGoals == match.AwayGoals + 1)
                        result = 0;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap +1.0 Fora":
                    if (match.AwayGoals >= match.HomeGoals)
                        result = stake * match.AwayHandicap1Odd - stake;
                    else if (match.AwayGoals - match.HomeGoals == -1)
                        result = 0;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap +0.75 Fora":
                    if (match.AwayGoals >= match.HomeGoals)
                        result = stake * match.AwayHandicap075Odd - stake;
                    else if (match.AwayGoals - match.HomeGoals == -1)
                        result = stake * 0.5 * -1;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap +0.5 Fora":
                    if (match.AwayGoals >= match.HomeGoals)
                        result = stake * match.AwayHandicap05Odd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap +0.25 Fora":
                    if (match.AwayGoals > match.HomeGoals)
                        result = stake * match.AwayHandicap025Odd - stake;
                    else if (match.AwayGoals == match.HomeGoals)
                        result = (stake * match.AwayHandicap025Odd - stake) * 0.5;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap 0.0 Fora":
                    if (match.AwayGoals > match.HomeGoals)
                        result = stake * match.AwayHandicap0Odd - stake;
                    else if (match.AwayGoals == match.HomeGoals)
                        result = 0;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap -0.25 Fora":
                    if (match.AwayGoals > match.HomeGoals)
                        result = stake * match.AwayHandicap025NegativeOdd - stake;
                    else if (match.AwayGoals == match.HomeGoals)
                        result = stake * 0.5 * -1;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap -0.5 Fora":
                    if (match.AwayGoals > match.HomeGoals)
                        result = stake * match.AwayHandicap05NegativeOdd - stake;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap -0.75 Fora":
                    if (match.AwayGoals > match.HomeGoals + 1)
                        result = stake * match.AwayHandicap075NegativeOdd - stake;
                    else if (match.AwayGoals == match.HomeGoals + 1)
                        result = (stake * match.AwayHandicap075NegativeOdd - stake) * 0.5;
                    else
                        result = stake * -1;

                    break;
                case "PUNTER - Handicap -1.0 Fora":
                    if (match.AwayGoals > match.HomeGoals + 1)
                        result = stake * match.AwayHandicap1NegativeOdd - stake;
                    else if (match.AwayGoals == match.HomeGoals + 1)
                        result = 0;
                    else
                        result = stake * -1;

                    break;
            }

            return result;
        }
    }
}

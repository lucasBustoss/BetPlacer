using BetPlacer.Punter.API.Models.Match;
using BetPlacer.Punter.API.Models.Strategy;
using System.Text.RegularExpressions;

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
                "TRADE - Vencer HT Casa",
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
                case "TRADE - Vencer HT Casa":
                    ranking.Add("Grupo 2");
                    ranking.Add("Grupo 1");
                    ranking.Add("Grupo 3");
                    ranking.Add("Grupo 4");

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
                case "TRADE - Vencer HT Casa":
                    if (match.HomeHTGoals > match.AwayHTGoals)
                    {
                        double greenMultiplicator = GetTradeGreenMultiplicator(match.HomeOdd);
                        result = stake * match.HomeOdd * greenMultiplicator - stake;

                    }
                    else if (match.AwayHTGoals > match.HomeHTGoals)
                    {
                        double redMultiplicator = GetTradeRedMultiplicator(match.HomeOdd);
                        result = stake * -1 * redMultiplicator;
                    }
                    else
                    {
                        double varianceMultiplicator = GetTradeVarianceMultiplicator(match.HomeOdd);
                        result = stake * -1 * varianceMultiplicator;
                    }

                    break;
                default:
                    break;
            }

            return result;
        }

        #region Private methods

        private static double GetTradeGreenMultiplicator(double odd)
        {
            double multiplicator = 1;

            switch (odd)
            {
                case double o when o > 1.01 && o <= 1.25:
                    multiplicator = 0.37;
                    break;
                case double o when o > 1.25 && o <= 1.5:
                    multiplicator = 0.41;
                    break;
                case double o when o > 1.5 && o <= 1.75:
                    multiplicator = 0.44;
                    break;
                case double o when o > 1.75 && o <= 1.90:
                    multiplicator = 0.47;
                    break;
                case double o when o > 1.9:
                    multiplicator = 0.5;
                    break;
            }

            return multiplicator;
        }

        private static double GetTradeRedMultiplicator(double odd)
        {
            double multiplicator = 1;

            switch (odd)
            {
                case double o when o > 1.01 && o <= 1.25:
                    multiplicator = 0.37;
                    break;
                case double o when o > 1.25 && o <= 1.5:
                    multiplicator = 0.44;
                    break;
                case double o when o > 1.5 && o <= 1.75:
                    multiplicator = 0.48;
                    break;
                case double o when o > 1.75 && o <= 1.90:
                    multiplicator = 0.55;
                    break;
                case double o when o > 1.9:
                    multiplicator = 0.6;
                    break;
            }

            return multiplicator;
        }

        private static double GetTradeVarianceMultiplicator(double odd)
        {
            double multiplicator = 1;

            switch (odd)
            {
                case double o when o > 1.01 && o <= 1.25:
                    multiplicator = 0.05;
                    break;
                case double o when o > 1.25 && o <= 1.5:
                    multiplicator = 0.1;
                    break;
                case double o when o > 1.5 && o <= 1.75:
                    multiplicator = 0.17;
                    break;
                case double o when o > 1.75 && o <= 1.90:
                    multiplicator = 0.22;
                    break;
                case double o when o > 1.9 && o <= 2.5:
                    multiplicator = 0.3;
                    break;
                case double o when o > 2.5:
                    multiplicator = 0.4;
                    break;
            }

            return multiplicator;
        }

        #endregion

    }
}

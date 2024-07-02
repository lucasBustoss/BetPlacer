using BetPlacer.Punter.API.Models.ValueObjects.Intervals;
using BetPlacer.Punter.API.Models.ValueObjects.Match;
using BetPlacer.Punter.API.Models.ValueObjects.Strategy;
using System.Text.RegularExpressions;

namespace BetPlacer.Punter.API.Utils
{
    public static class VariableCalculator
    {
        public static void CalculateBestVariables(List<StrategyInfo> strategies, List<MatchBarCode> matches, double stake)
        {
            List<string> variables = GetVariableProperties();

            foreach (StrategyInfo strategy in strategies)
            {
                List<BestInterval> bestIntervals = new List<BestInterval>();

                foreach (string variable in variables)
                {
                    List<Tuple<double, double>> variableByResult = new List<Tuple<double, double>>();

                    foreach (MatchAnalyzed matchAnalyzed in strategy.Matches)
                    {
                        MatchBarCode match = matches.Where(m => m.MatchCode == matchAnalyzed.MatchCode).FirstOrDefault();

                        double variableValue = GetVariableValue(match, variable);
                        double matchResult = StrategyUtils.GetMatchResult(matchAnalyzed, strategy.Name, stake);

                        variableByResult.Add(Tuple.Create(variableValue, matchResult));
                    }

                    List<Tuple<double, double>> selectedBestIntervals = new List<Tuple<double, double>>();

                    for (int i = 1; i <= 5; i++)
                    {
                        List<VariableInterval> groupedVariableResults = GroupedVariableResults(variableByResult, i, 1);
                        Tuple<double, double> bestInterval = GetBestInterval(groupedVariableResults, strategy.Matches.Count);

                        if (bestInterval != null)
                            selectedBestIntervals.Add(bestInterval);
                    }

                    if (selectedBestIntervals.Count > 0)
                    {
                        foreach (Tuple<double, double> bestInterval in selectedBestIntervals)
                        {
                            BestInterval interval = GetBestIntervalInfo(strategy, bestInterval, matches, variable, stake);
                            bestIntervals.Add(interval);
                        }
                    }
                }

                List<BestInterval> filteredIntervals = bestIntervals
                    .Where(bi => bi.InferiorLimit > 0)
                    .OrderBy(bi => bi.CoefficientVariation)
                    .ThenByDescending(bi => bi.InferiorLimit)
                    .GroupBy(obj => obj.PropertyName)
                    .Select(group => group.OrderBy(obj => obj.CoefficientVariation).First())
                    .ToList();

                strategy.BestIntervals = filteredIntervals;
                strategy.ResultAfterIntervals = GetResultWithIntervalsApplied(strategy, matches, stake);
            }
        }

        #region Private methods

        private static List<string> GetVariableProperties()
        {
            return new List<string>()
            {
                "HomeOdd",
                "DrawOdd",
                "AwayOdd",
                "Over25Odd",
                "Under25Odd",
                "BttsYesOdd",
                "BttsNoOdd",
                "PowerPoint",
                "PowerPointHT",
                "CVMatchOdds",
                "HomeCVPoints",
                "HomeCVPointsHT",
                "HomePoints",
                "HomePointsHT",
                "HomeDifferenceGoals",
                "HomeDifferenceGoalsHT",
                "HomeCVDifferenceGoals",
                "HomeCVDifferenceGoalsHT",
                "HomePoisson",
                "HomePoissonHT",
                "HomeGoalsScored",
                "HomeGoalsScoredValue",
                "HomeGoalsScoredCost",
                "HomeGoalsScoredCV",
                "HomeGoalsScoredHT",
                "HomeGoalsScoredValueHT",
                "HomeGoalsScoredCostHT",
                "HomeGoalsScoredCVHT",
                "HomeGoalsConceded",
                "HomeGoalsConcededValue",
                "HomeGoalsConcededCost",
                "HomeGoalsConcededCV",
                "HomeGoalsConcededHT",
                "HomeGoalsConcededValueHT",
                "HomeGoalsConcededCostHT",
                "HomeGoalsConcededCVHT",
                "HomeOddsCV",
                "HomeMatchOddsRPS",
                "HomeMatchOddsHTRPS",
                "HomeGoalsRPS",
                "HomeBTTSRPS",
                "AwayCVPoints",
                "AwayCVPointsHT",
                "AwayPoints",
                "AwayPointsHT",
                "AwayDifferenceGoals",
                "AwayDifferenceGoalsHT",
                "AwayCVDifferenceGoals",
                "AwayCVDifferenceGoalsHT",
                "AwayPoisson",
                "AwayPoissonHT",
                "AwayGoalsScored",
                "AwayGoalsScoredValue",
                "AwayGoalsScoredCost",
                "AwayGoalsScoredCV",
                "AwayGoalsScoredHT",
                "AwayGoalsScoredValueHT",
                "AwayGoalsScoredCostHT",
                "AwayGoalsScoredCVHT",
                "AwayGoalsConceded",
                "AwayGoalsConcededValue",
                "AwayGoalsConcededCost",
                "AwayGoalsConcededCV",
                "AwayGoalsConcededHT",
                "AwayGoalsConcededValueHT",
                "AwayGoalsConcededCostHT",
                "AwayGoalsConcededCVHT",
                "AwayOddsCV",
                "AwayMatchOddsRPS",
                "AwayMatchOddsHTRPS",
                "AwayGoalsRPS",
                "AwayBTTSRPS"
            };
        }

        private static double GetVariableValue(MatchBarCode obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
            {
                var propertyValue = propertyInfo.GetValue(obj);

                // Verifica se o valor da propriedade é nulo
                if (propertyValue != null && propertyValue is double)
                    return (double)propertyValue;
                else
                    return -1;
            }

            return -1;
        }

        private static List<VariableInterval> GroupedVariableResults(List<Tuple<double, double>> variableResults, double multiplicator = 1, double maxValue = 1)
        {
            List<VariableInterval> intervals = new List<VariableInterval>();
            double accumulateResult = 0;
            double decimalMultiplicator = multiplicator / 100;

            for (double initialValue = 0; initialValue < maxValue; initialValue += decimalMultiplicator)
            {
                double finalValue = initialValue + decimalMultiplicator;
                List<double> matchesResult = variableResults.Where(vr => vr.Item1 >= initialValue && vr.Item1 < finalValue).Select(vr => vr.Item2).ToList();
                double result = matchesResult.Sum();
                accumulateResult += result;

                VariableInterval interval = new VariableInterval($"{Math.Round(initialValue, 2)} - {Math.Round(finalValue, 2)}", result, accumulateResult, matchesResult.Count);
                intervals.Add(interval);
            }

            return intervals;
        }

        private static Tuple<double, double> GetBestInterval(List<VariableInterval> variableIntervals, int totalMatches)
        {
            double bestAccumulate = 0;
            double currentAccumulate = 0;
            double lastFinalValue = 0;

            double initialValue = -1;
            double finalValue = -1;

            double validateInitialValue = 0;
            double validateFinalValue = 0;

            int matchesInFilter = 0;

            int maxTolerate = 1;
            int currentTolerate = 0;

            bool shouldSetInitialValue = true;
            bool shouldValidateValues = false;

            foreach (VariableInterval variableInterval in variableIntervals)
            {
                double result = variableInterval.Result;

                if (result <= 0)
                {
                    if (currentTolerate == maxTolerate)
                    {
                        finalValue = lastFinalValue;
                        shouldSetInitialValue = true;
                        shouldValidateValues = true;
                        currentTolerate++;
                    }
                    else if (currentTolerate < maxTolerate)
                    {
                        currentAccumulate += result;
                        matchesInFilter += variableInterval.MatchesCount;
                        currentTolerate++;
                    }
                }
                else
                {
                    if (shouldSetInitialValue)
                    {
                        initialValue = Convert.ToDouble(variableInterval.Interval.Split(" - ")[0]);
                        shouldSetInitialValue = false;
                    }

                    currentAccumulate += result;
                    currentTolerate = 0;
                    matchesInFilter += variableInterval.MatchesCount;
                }

                if (shouldValidateValues)
                {
                    shouldValidateValues = false;

                    double qtdPercentMatchesInFilter = (double)matchesInFilter / (double)totalMatches;

                    if (currentAccumulate > bestAccumulate && qtdPercentMatchesInFilter >= 0.4 && qtdPercentMatchesInFilter <= 0.9)
                    {
                        validateInitialValue = initialValue;
                        validateFinalValue = finalValue;
                    }

                    matchesInFilter = 0;
                }

                lastFinalValue = Convert.ToDouble(variableInterval.Interval.Split(" - ")[1]);
            }

            if (validateInitialValue != 0 && validateFinalValue != 0)
                return Tuple.Create(validateInitialValue, validateFinalValue);

            return null;
        }

        private static BestInterval GetBestIntervalInfo(StrategyInfo strategy, Tuple<double, double> interval, List<MatchBarCode> matches, string property, double stake)
        {
            List<Tuple<double, double>> resultVariables = new List<Tuple<double, double>>();
            List<int> matchClassifiedCodes = strategy.Matches.Select(m => m.MatchCode).ToList();

            List<MatchBarCode> filteredList = matches.Where(obj => matchClassifiedCodes.Contains(obj.MatchCode)).ToList();

            List<MatchBarCode> selectedMatches = filteredList.Where(m =>
            {
                var propertyValue = GetVariableValue(m, property);
                return propertyValue >= interval.Item1 && propertyValue <= interval.Item2;
            }).ToList();

            foreach (MatchBarCode matchBarCode in selectedMatches)
            {
                MatchAnalyzed matchAnalyzed = strategy.Matches.Where(m => m.MatchCode == matchBarCode.MatchCode).FirstOrDefault();

                double propertyValue = GetVariableValue(matchBarCode, property);
                double result = StrategyUtils.GetMatchResult(matchAnalyzed, strategy.Name, stake);

                Tuple<double, double> resultVariable = Tuple.Create(propertyValue, result);
                resultVariables.Add(resultVariable);
            }

            double stdResult = MathUtils.StandardDeviation(resultVariables.Select(rv => rv.Item2));
            double avgResult = resultVariables.Select(rv => rv.Item2).Average();
            double cvResult = stdResult / avgResult;

            double confidence = MathUtils.Confidence(0.05, stdResult, resultVariables.Count);
            double inferiorLimit = avgResult - confidence;

            BestInterval bestInterval = new BestInterval(property, interval.Item1, interval.Item2, cvResult, inferiorLimit);

            return bestInterval;
        }

        private static List<ResultInterval> GetResultWithIntervalsApplied(StrategyInfo strategy, List<MatchBarCode> matches, double stake)
        {
            List<ResultInterval> resultIntervals = new List<ResultInterval>();
            List<int> matchClassifiedCodes = strategy.Matches.Select(m => m.MatchCode).ToList();
            List<MatchBarCode> filteredList = matches.Where(obj => matchClassifiedCodes.Contains(obj.MatchCode)).ToList();
            string composeName = "";

            int lastTotalMatches = 0;
            double lastResult = 0;

            bool isFirst = true;

            foreach (BestInterval bestInterval in strategy.BestIntervals)
            {
                filteredList = filteredList.Where(m =>
                {
                    var propertyValue = GetVariableValue(m, bestInterval.PropertyName);
                    return propertyValue >= bestInterval.InitialInterval && propertyValue <= bestInterval.FinalInterval;
                }).ToList();

                List<int> filteredListCodes = filteredList.Select(fl => fl.MatchCode).ToList();
                List<MatchAnalyzed> matchesAnalyzed = strategy.Matches.Where(obj => filteredListCodes.Contains(obj.MatchCode)).ToList();

                List<double> results = StrategyUtils.GetMatchResults(matchesAnalyzed, strategy.Name, stake).Values.SelectMany(list => list).ToList();
                double result = results.Sum() / stake;

                int totalMatches = filteredList.Count;

                if (totalMatches != lastTotalMatches && result != lastResult && totalMatches > (matches.Count * 0.05))
                {
                    var stdResult = MathUtils.StandardDeviation(results);
                    var avgResult = results.Average();
                    var cvResult = stdResult / avgResult;

                    var confidence = MathUtils.Confidence(0.05, stdResult, totalMatches);
                    var inferiorLimit = avgResult - confidence;

                    if (isFirst)
                    {
                        composeName += $"{bestInterval.PropertyName}";
                        isFirst = false;
                    }
                    else
                        composeName += $" - {bestInterval.PropertyName}";

                    ResultInterval resultInterval = new ResultInterval(composeName, (double)totalMatches / (double)matches.Count, result, cvResult, inferiorLimit, false, 0);

                    resultIntervals.Add(resultInterval);

                    lastResult = result;
                    lastTotalMatches = totalMatches;
                }

            }

            return resultIntervals;
        }

        #endregion
    }
}

using BetPlacer.Punter.API.Models;
using BetPlacer.Punter.API.Models.Match;
using BetPlacer.Punter.API.Models.Match.Team;
using BetPlacer.Punter.API.Models.Strategy;
using BetPlacer.Punter.API.Utils;
using System.Text.RegularExpressions;

namespace BetPlacer.Punter.API.Services
{
    public class CalculateStatsService
    {
        public void CalculateStats(List<MatchBaseData> matchBaseData)
        {
            #region Base data

            List<TeamAverageData> teamsAverageData = new List<TeamAverageData>();

            List<TeamMatchesBySeason> teamMatches = matchBaseData
                .Where(mbd => mbd.Season == "2013" || mbd.Season == "2013-2014")
                .SelectMany(m => new[]
                {
                    new { TeamName = m.HomeTeam, Season = m.Season, Match = m },
                    new { TeamName = m.AwayTeam, Season = m.Season, Match = m }
                })
                .GroupBy(t => new { t.TeamName, t.Season })
                .Select(g => new TeamMatchesBySeason
                {
                    TeamName = g.Key.TeamName,
                    Matches = g.Select(x => x.Match).ToList()
                })
                .ToList();

            foreach (TeamMatchesBySeason teamSeason in teamMatches)
            {
                TeamAverageData averageData = new TeamAverageData(teamSeason.TeamName);
                var last10Matches = teamSeason.Matches.ToList();
                //var last10Matches = teamSeason.Matches.TakeLast(10).ToList();
                CalculateAverage(averageData, last10Matches);

                teamsAverageData.Add(averageData);
            }

            List<MatchBaseData> matchesInOtherSeasons = matchBaseData
                .Where(mbd => mbd.Season != "2013" && mbd.Season != "2013-2014").ToList();

            #endregion

            #region Bar code

            List<MatchBarCode> matchesBarCode = new List<MatchBarCode>();
            List<MatchBarCode> matchesKNN = new List<MatchBarCode>();

            for (int i = 0; i < matchesInOtherSeasons.Count; i++)
            {
                MatchBaseData match = matchesInOtherSeasons[i];

                MatchBarCode matchBarCode = GetMatchBarCode(match, teamsAverageData, i);
                matchesBarCode.Add(matchBarCode);
                
                MatchBarCode matchBarCodeToKNN = GetMatchBarCode(match, teamsAverageData, i);
                matchBarCodeToKNN.NormalizeValues();
                matchesKNN.Add(matchBarCodeToKNN);

                UpdateTeamAverageData(teamsAverageData, teamMatches, match, true);
                UpdateTeamAverageData(teamsAverageData, teamMatches, match, false);
            }

            #endregion

            #region KNN

            List<MatchAnalyzed> matchesAnalyzed = CalculateKNN(matchesKNN, matchesInOtherSeasons);

            #endregion

            #region Results

            double stake = 100;
            int totalMatches = matchesAnalyzed.Count;
            List<Strategy> strategies = CalculateResults(matchesAnalyzed, stake);

            foreach (Strategy strategy in strategies)
            {
                var filteredClassifications = strategy.StrategyClassifications.Where(sc => sc.ProfitLoss > -5 && sc.HistoricalCoefficientVariation <= 0.3).ToList();
                strategy.StrategyClassifications = filteredClassifications;
            }

            strategies = strategies.Where(s => s.StrategyClassifications.Count > 0 && s.StrategyClassifications.Select(sc => sc.TotalMatches).Sum() > totalMatches * 0.25).ToList();
            List<StrategyInfo> strategyInfos = new List<StrategyInfo>();

            if (strategies.Count > 0)
            {
                foreach (Strategy strategy in strategies)
                {
                    StrategyInfo strategyInfo = FilterMatchesByClassifications(strategy, matchesAnalyzed, stake);

                    if (strategyInfo != null) 
                        strategyInfos.Add(strategyInfo);
                }
            }

            #endregion

            if (strategyInfos.Count > 0)
                VariableCalculator.CalculateBestVariables(strategyInfos, matchesBarCode, stake);
        }

        #region Private methods

        #region Averages

        private void CalculateAverage(TeamAverageData averageData, List<MatchBaseData> matches)
        {
            var last10MatchesHome = matches.Where(m => m.HomeTeam == averageData.TeamName).ToList();
            var last10MatchesAway = matches.Where(m => m.AwayTeam == averageData.TeamName).ToList();

            CalculateScoredAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculateConcededAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculatePointsAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculateDifferenceGoalsAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculateOddsAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculateRPS(averageData, last10MatchesHome, last10MatchesAway);
        }

        private void CalculateScoredAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            double sumHomePercentageOdd = awayMatches.Sum(ma => ma.HomePercentageOdd);
            double sumAwayPercentageOdd = homeMatches.Sum(mh => mh.AwayPercentageOdd);

            #region Overall

            #region Home

            double sumHomeScoredGoalsValue = homeMatches.Sum(mh => mh.HomeScoredGoalValue);
            double sumHomeScoredGoalsCost = homeMatches.Sum(mh => mh.HomeScoredGoalCost);

            double stdHomeGoalScored = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.HomeGoals).ToList());

            double avgHomeGoalScored = sumAwayPercentageOdd != 0 ? sumHomeScoredGoalsValue / sumAwayPercentageOdd : 0;
            double avgHomeGoalScoredValue = homeMatches.Count() != 0 ? sumHomeScoredGoalsValue / homeMatches.Count() : 0;
            double avgHomeGoalScoredCost = homeMatches.Count() != 0 ? sumHomeScoredGoalsCost / homeMatches.Count() : 0;

            averageData.HomeGoalScoredAvg = avgHomeGoalScored;
            averageData.HomeGoalScoredStd = stdHomeGoalScored;
            averageData.HomeGoalScoredCv = avgHomeGoalScored != 0 ? stdHomeGoalScored / avgHomeGoalScored : 0;
            averageData.HomeGoalScoredValueAvg = avgHomeGoalScoredValue;
            averageData.HomeGoalScoredCostAvg = avgHomeGoalScoredCost;

            #endregion

            #region Away

            double sumAwayScoredGoalsValue = awayMatches.Sum(ma => ma.AwayScoredGoalValue);
            double sumAwayScoredGoalsCost = awayMatches.Sum(ma => ma.AwayScoredGoalCost);

            double stdAwayGoalScored = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoals).ToList());

            double avgAwayGoalScored = sumHomePercentageOdd != 0 ? sumAwayScoredGoalsValue / sumHomePercentageOdd : 0;
            double avgAwayGoalScoredValue = awayMatches.Count() != 0 ? sumAwayScoredGoalsValue / awayMatches.Count() : 0;
            double avgAwayGoalScoredCost = awayMatches.Count() != 0 ? sumAwayScoredGoalsCost / awayMatches.Count() : 0;

            averageData.AwayGoalScoredAvg = avgAwayGoalScored;
            averageData.AwayGoalScoredStd = stdAwayGoalScored;
            averageData.AwayGoalScoredCv = avgAwayGoalScored != 0 ? stdAwayGoalScored / avgAwayGoalScored : avgAwayGoalScored;
            averageData.AwayGoalScoredValueAvg = avgAwayGoalScoredValue;
            averageData.AwayGoalScoredCostAvg = avgAwayGoalScoredCost;

            #endregion

            #endregion

            #region HT

            #region Home

            double sumHomeScoredGoalsValueHT = homeMatches.Sum(mh => mh.HomeScoredGoalValueHT);
            double sumHomeScoredGoalsCostHT = homeMatches.Sum(mh => mh.HomeScoredGoalCostHT);

            double stdHomeGoalScoredHT = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.HomeGoalsHT).ToList());

            double avgHomeGoalScoredHT = sumAwayPercentageOdd != 0 ? sumHomeScoredGoalsValueHT / sumAwayPercentageOdd : 0;
            double avgHomeGoalScoredValueHT = homeMatches.Count() != 0 ? sumHomeScoredGoalsValueHT / homeMatches.Count() : 0;
            double avgHomeGoalScoredCostHT = homeMatches.Count() != 0 ? sumHomeScoredGoalsCostHT / homeMatches.Count() : 0;

            averageData.HomeGoalScoredHTAvg = avgHomeGoalScoredHT;
            averageData.HomeGoalScoredHTStd = stdHomeGoalScoredHT;
            averageData.HomeGoalScoredHTCv = avgHomeGoalScoredHT != 0 ? stdHomeGoalScoredHT / avgHomeGoalScoredHT : avgHomeGoalScoredHT;
            averageData.HomeGoalScoredValueHTAvg = avgHomeGoalScoredValueHT;
            averageData.HomeGoalScoredCostHTAvg = avgHomeGoalScoredCostHT;

            #endregion

            #region Away

            double sumAwayScoredGoalsValueHT = awayMatches.Sum(ma => ma.AwayScoredGoalValueHT);
            double sumAwayScoredGoalsCostHT = awayMatches.Sum(ma => ma.AwayScoredGoalCostHT);

            double stdAwayGoalScoredHT = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoalsHT).ToList());

            double avgAwayGoalScoredHT = sumHomePercentageOdd != 0 ? sumAwayScoredGoalsValueHT / sumHomePercentageOdd : 0;
            double avgAwayGoalScoredValueHT = awayMatches.Count() != 0 ? sumAwayScoredGoalsValueHT / awayMatches.Count() : 0;
            double avgAwayGoalScoredCostHT = awayMatches.Count() != 0 ? sumAwayScoredGoalsCostHT / awayMatches.Count() : 0;

            averageData.AwayGoalScoredHTAvg = avgAwayGoalScoredHT;
            averageData.AwayGoalScoredHTStd = stdAwayGoalScoredHT;
            averageData.AwayGoalScoredHTCv = avgAwayGoalScoredHT != 0 ? stdAwayGoalScoredHT / avgAwayGoalScoredHT : avgAwayGoalScoredHT;
            averageData.AwayGoalScoredValueHTAvg = avgAwayGoalScoredValueHT;
            averageData.AwayGoalScoredCostHTAvg = avgAwayGoalScoredCostHT;

            #endregion

            #endregion
        }

        private void CalculateConcededAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            #region Overall

            #region Home

            double sumHomeConcededGoalsValue = homeMatches.Sum(mh => mh.HomeConcededGoalValue);
            double sumHomeConcededGoalsCost = homeMatches.Sum(mh => mh.HomeConcededGoalCost);
            double sumAwayPercentageOdd = homeMatches.Sum(mh => mh.AwayPercentageOdd);

            double stdHomeGoalConcededValue = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.AwayGoals).ToList());

            double avgHomeGoalConcededValue = sumAwayPercentageOdd == 0 ? 0 : sumHomeConcededGoalsValue / sumAwayPercentageOdd;

            averageData.HomeGoalConcededAvg = avgHomeGoalConcededValue;
            averageData.HomeGoalConcededStd = stdHomeGoalConcededValue;
            averageData.HomeGoalConcededCv = avgHomeGoalConcededValue == 0 ? 0 : stdHomeGoalConcededValue / avgHomeGoalConcededValue;
            averageData.HomeGoalConcededValueAvg = homeMatches.Count() == 0 ? 0 : sumHomeConcededGoalsValue / homeMatches.Count();
            averageData.HomeGoalConcededCostAvg = homeMatches.Count() == 0 ? 0 : sumHomeConcededGoalsCost / homeMatches.Count();

            #endregion

            #region Away

            double sumAwayConcededGoalsValue = awayMatches.Sum(mh => mh.AwayConcededGoalValue);
            double sumAwayConcededGoalsCost = awayMatches.Sum(mh => mh.AwayConcededGoalCost);
            double sumHomePercentageOdd = awayMatches.Sum(mh => mh.HomePercentageOdd);

            double stdAwayGoalConcededValue = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.HomeGoals).ToList());

            double avgAwayGoalConcededValue = sumHomePercentageOdd == 0 ? 0 : sumAwayConcededGoalsValue / sumHomePercentageOdd;

            averageData.AwayGoalConcededAvg = avgAwayGoalConcededValue;
            averageData.AwayGoalConcededStd = stdAwayGoalConcededValue;
            averageData.AwayGoalConcededCv = avgAwayGoalConcededValue == 0 ? 0 : stdAwayGoalConcededValue / avgAwayGoalConcededValue;
            averageData.AwayGoalConcededValueAvg = awayMatches.Count() == 0 ? 0 : sumAwayConcededGoalsValue / awayMatches.Count();
            averageData.AwayGoalConcededCostAvg = awayMatches.Count() == 0 ? 0 : sumAwayConcededGoalsCost / awayMatches.Count();

            #endregion

            #endregion

            #region HT

            #region Home

            double sumHomeConcededGoalsValueHT = homeMatches.Sum(mh => mh.HomeConcededGoalValueHT);
            double sumHomeConcededGoalsCostHT = homeMatches.Sum(mh => mh.HomeConcededGoalCostHT);
            double sumAwayPercentageOddHT = homeMatches.Sum(mh => mh.AwayPercentageOdd);

            double stdHomeGoalConcededValueHT = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.AwayGoalsHT).ToList());

            double avgHomeGoalConcededValueHT = sumAwayPercentageOddHT == 0 ? 0 : sumHomeConcededGoalsValueHT / sumAwayPercentageOddHT;

            averageData.HomeGoalConcededHTAvg = avgHomeGoalConcededValueHT;
            averageData.HomeGoalConcededHTStd = stdHomeGoalConcededValueHT;
            averageData.HomeGoalConcededHTCv = avgHomeGoalConcededValueHT == 0 ? 0 : stdHomeGoalConcededValueHT / avgHomeGoalConcededValueHT;
            averageData.HomeGoalConcededValueHTAvg = homeMatches.Count() == 0 ? 0 : sumHomeConcededGoalsValueHT / homeMatches.Count();
            averageData.HomeGoalConcededCostHTAvg = homeMatches.Count() == 0 ? 0 : sumHomeConcededGoalsCostHT / homeMatches.Count();

            #endregion

            #region Away

            double sumAwayConcededGoalsValueHT = awayMatches.Sum(mh => mh.AwayConcededGoalValueHT);
            double sumAwayConcededGoalsCostHT = awayMatches.Sum(mh => mh.AwayConcededGoalCostHT);
            double sumHomePercentageOddHT = awayMatches.Sum(mh => mh.HomePercentageOdd);

            double stdAwayGoalConcededValueHT = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.HomeGoalsHT).ToList());

            double avgAwayGoalConcededValueHT = sumHomePercentageOddHT == 0 ? 0 : sumAwayConcededGoalsValueHT / sumHomePercentageOddHT;

            averageData.AwayGoalConcededHTAvg = avgAwayGoalConcededValueHT;
            averageData.AwayGoalConcededHTStd = stdAwayGoalConcededValueHT;
            averageData.AwayGoalConcededHTCv = avgAwayGoalConcededValueHT == 0 ? 0 : stdAwayGoalConcededValueHT / avgAwayGoalConcededValueHT;
            averageData.AwayGoalConcededValueHTAvg = awayMatches.Count() == 0 ? 0 : sumAwayConcededGoalsValueHT / awayMatches.Count();
            averageData.AwayGoalConcededCostHTAvg = awayMatches.Count() == 0 ? 0 : sumAwayConcededGoalsCostHT / awayMatches.Count();

            #endregion

            #endregion
        }

        private void CalculatePointsAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            double sumAwayPercentageOdd = homeMatches.Sum(hm => hm.AwayPercentageOdd);
            double sumHomePercentageOdd = awayMatches.Sum(am => am.HomePercentageOdd);

            #region Overall

            #region Home

            double sumHomePointsValue = homeMatches.Sum(hm => hm.HomePointsValue);

            double stdHomePoints = MathUtils.StandardDeviation(homeMatches.Select(hm => (double)hm.HomePoints).ToList());

            double avgHomePointsValue = sumAwayPercentageOdd == 0 ? 0 : sumHomePointsValue / sumAwayPercentageOdd;

            averageData.HomePointsAvg = avgHomePointsValue;
            averageData.HomePointsStd = stdHomePoints;
            averageData.HomePointsCv = avgHomePointsValue == 0 ? 0 : stdHomePoints / avgHomePointsValue;

            #endregion

            #region Away

            double sumAwayPointsValue = awayMatches.Sum(am => am.AwayPointsValue);

            double stdAwayPoints = MathUtils.StandardDeviation(awayMatches.Select(am => (double)am.AwayPoints).ToList());

            double avgAwayPointsValue = sumHomePercentageOdd == 0 ? 0 : sumAwayPointsValue / sumHomePercentageOdd;

            averageData.AwayPointsAvg = avgAwayPointsValue;
            averageData.AwayPointsStd = stdAwayPoints;
            averageData.AwayPointsCv = avgAwayPointsValue == 0 ? 0 : stdAwayPoints / avgAwayPointsValue;

            #endregion

            #endregion

            #region HT

            #region Home

            double sumHomePointsValueHT = homeMatches.Sum(hm => hm.HomePointsValueHT);

            double stdHomePointsHT = MathUtils.StandardDeviation(homeMatches.Select(hm => (double)hm.HomePointsHT).ToList());

            double avgHomePointsValueHT = sumAwayPercentageOdd == 0 ? 0 : sumHomePointsValueHT / sumAwayPercentageOdd;

            averageData.HomePointsHTAvg = avgHomePointsValueHT;
            averageData.HomePointsHTStd = stdHomePointsHT;
            averageData.HomePointsHTCv = avgHomePointsValueHT == 0 ? 0 : stdHomePointsHT / avgHomePointsValueHT;

            #endregion

            #region Away

            double sumAwayPointsValueHT = awayMatches.Sum(am => am.AwayPointsValueHT);

            double stdAwayPointsHT = MathUtils.StandardDeviation(awayMatches.Select(am => (double)am.AwayPointsHT).ToList());

            double avgAwayPointsValueHT = sumHomePercentageOdd == 0 ? 0 : sumAwayPointsValueHT / sumHomePercentageOdd;

            averageData.AwayPointsHTAvg = avgAwayPointsValueHT;
            averageData.AwayPointsHTStd = stdAwayPointsHT;
            averageData.AwayPointsHTCv = avgAwayPointsValueHT == 0 ? 0 : stdAwayPointsHT / avgAwayPointsValueHT;

            #endregion

            #endregion
        }

        private void CalculateDifferenceGoalsAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            double sumAwayPercentageOdd = homeMatches.Sum(mh => mh.AwayPercentageOdd);
            double sumHomePercentageOdd = awayMatches.Sum(mh => mh.HomePercentageOdd);

            #region Overall

            #region Home

            double sumHomeGoalsDifferenceValue = homeMatches.Sum(mh => mh.HomeGoalsDifferenceValue);

            double stdHomeGoalsDifferenceValue = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.HomeGoalsDifference).ToList());

            double avgHomeGoalDifferenceValue = sumAwayPercentageOdd == 0 ? 0 : sumHomeGoalsDifferenceValue / sumAwayPercentageOdd;

            averageData.HomeGoalsDifferenceAvg = avgHomeGoalDifferenceValue;
            averageData.HomeGoalsDifferenceStd = stdHomeGoalsDifferenceValue;
            averageData.HomeGoalsDifferenceCv = avgHomeGoalDifferenceValue == 0 ? 0 : Math.Abs(Math.Round(stdHomeGoalsDifferenceValue / avgHomeGoalDifferenceValue));

            #endregion

            #region Away

            double sumAwayGoalsDifferenceValue = awayMatches.Sum(mh => mh.AwayGoalsDifferenceValue);

            double stdAwayGoalsDifferenceValue = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoalsDifference).ToList());

            double avgAwayGoalDifferenceValue = sumHomePercentageOdd == 0 ? 0 : sumAwayGoalsDifferenceValue / sumHomePercentageOdd;

            averageData.AwayGoalsDifferenceAvg = avgAwayGoalDifferenceValue;
            averageData.AwayGoalsDifferenceStd = stdAwayGoalsDifferenceValue;
            averageData.AwayGoalsDifferenceCv = avgAwayGoalDifferenceValue == 0 ? 0 : Math.Abs(Math.Round(stdAwayGoalsDifferenceValue / avgAwayGoalDifferenceValue));

            #endregion

            #endregion

            #region HT

            #region Home

            double sumHomeGoalsDifferenceValueHT = homeMatches.Sum(mh => mh.HomeGoalsDifferenceValueHT);

            double stdHomeGoalsDifferenceValueHT = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.HomeGoalsDifferenceHT).ToList());

            double avgHomeGoalDifferenceValueHT = sumAwayPercentageOdd == 0 ? 0 : sumHomeGoalsDifferenceValueHT / sumAwayPercentageOdd;

            averageData.HomeGoalsDifferenceHTAvg = avgHomeGoalDifferenceValueHT;
            averageData.HomeGoalsDifferenceHTStd = stdHomeGoalsDifferenceValueHT;
            averageData.HomeGoalsDifferenceHTCv = avgHomeGoalDifferenceValueHT == 0 ? 0 : Math.Abs(Math.Round(stdHomeGoalsDifferenceValueHT / avgHomeGoalDifferenceValueHT));

            #endregion

            #region Away

            double sumAwayGoalsDifferenceValueHT = awayMatches.Sum(mh => mh.AwayGoalsDifferenceValueHT);

            double stdAwayGoalsDifferenceValueHT = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoalsDifferenceHT).ToList());

            double avgAwayGoalDifferenceValueHT = sumHomePercentageOdd == 0 ? 0 : sumAwayGoalsDifferenceValueHT / sumHomePercentageOdd;

            averageData.AwayGoalsDifferenceHTAvg = avgAwayGoalDifferenceValueHT;
            averageData.AwayGoalsDifferenceHTStd = stdAwayGoalsDifferenceValueHT;
            averageData.AwayGoalsDifferenceHTCv = avgAwayGoalDifferenceValueHT == 0 ? 0 : Math.Abs(Math.Round(stdAwayGoalsDifferenceValueHT / avgAwayGoalDifferenceValueHT));

            #endregion

            #endregion
        }

        private void CalculateOddsAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            #region Home

            var sumHomeOdds = homeMatches.Sum(hm => hm.HomeOdd);

            var stdHomeOdds = MathUtils.StandardDeviation(homeMatches.Select(hm => hm.HomeOdd).ToList());

            var avgHomeOdds = homeMatches.Count() == 0 ? 0 : sumHomeOdds / homeMatches.Count();

            averageData.HomeOddsAvg = avgHomeOdds;
            averageData.HomeOddsStd = stdHomeOdds;
            averageData.HomeOddsCv = avgHomeOdds == 0 ? 0 : stdHomeOdds / avgHomeOdds;

            #endregion

            #region Away

            var sumAwayOdds = awayMatches.Sum(hm => hm.AwayOdd);

            var stdAwayOdds = MathUtils.StandardDeviation(awayMatches.Select(hm => hm.AwayOdd).ToList());

            var avgAwayOdds = awayMatches.Count() == 0 ? 0 : sumAwayOdds / awayMatches.Count();

            averageData.AwayOddsAvg = avgAwayOdds;
            averageData.AwayOddsStd = stdAwayOdds;
            averageData.AwayOddsCv = avgAwayOdds == 0 ? 0 : stdAwayOdds / avgAwayOdds;

            #endregion
        }

        private void CalculateRPS(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            double homeRPSMOTotal = 0;
            double homeRPSMOHTTotal = 0;
            double homeRPSGoalsTotal = 0;
            double homeRPSBTTSTotal = 0;

            double awayRPSMOTotal = 0;
            double awayRPSMOHTTotal = 0;
            double awayRPSGoalsTotal = 0;
            double awayRPSBTTSTotal = 0;

            foreach (var homeMatch in homeMatches)
            {
                homeRPSMOTotal += CalculateRPSMO(homeMatch.HomeOdd, homeMatch.DrawOdd, homeMatch.AwayOdd, homeMatch.MatchResult);
                homeRPSMOHTTotal += CalculateRPSMO(homeMatch.HomeOdd, homeMatch.DrawOdd, homeMatch.AwayOdd, homeMatch.MatchResultHT);
                homeRPSGoalsTotal += CalculateRPSGoals(homeMatch.Over25Odd, homeMatch.Under25Odd, homeMatch.GoalsResult);
                homeRPSBTTSTotal += CalculateRPSBTTS(homeMatch.BttsYesOdd, homeMatch.BttsNoOdd, homeMatch.BttsResult);
            }

            averageData.HomeRPSMO = homeMatches.Count == 0 ? 0 : homeRPSMOTotal / homeMatches.Count;
            averageData.HomeRPSMOHT = homeMatches.Count == 0 ? 0 : homeRPSMOHTTotal / homeMatches.Count;
            averageData.HomeRPSGoals = homeMatches.Count == 0 ? 0 : homeRPSGoalsTotal / homeMatches.Count;
            averageData.HomeRPSBTTS = homeMatches.Count == 0 ? 0 : homeRPSBTTSTotal / homeMatches.Count;

            foreach (var awayMatch in awayMatches)
            {
                awayRPSMOTotal += CalculateRPSMO(awayMatch.HomeOdd, awayMatch.DrawOdd, awayMatch.AwayOdd, awayMatch.MatchResult);
                awayRPSMOHTTotal += CalculateRPSMO(awayMatch.HomeOdd, awayMatch.DrawOdd, awayMatch.AwayOdd, awayMatch.MatchResultHT);
                awayRPSGoalsTotal += CalculateRPSGoals(awayMatch.Over25Odd, awayMatch.Under25Odd, awayMatch.GoalsResult);
                awayRPSBTTSTotal += CalculateRPSBTTS(awayMatch.BttsYesOdd, awayMatch.BttsNoOdd, awayMatch.BttsResult);
            }

            averageData.AwayRPSMO = awayMatches.Count == 0 ? 0 : awayRPSMOTotal / awayMatches.Count;
            averageData.AwayRPSMOHT = awayMatches.Count == 0 ? 0 : awayRPSMOHTTotal / awayMatches.Count;
            averageData.AwayRPSGoals = awayMatches.Count == 0 ? 0 : awayRPSGoalsTotal / awayMatches.Count;
            averageData.AwayRPSBTTS = awayMatches.Count == 0 ? 0 : awayRPSBTTSTotal / awayMatches.Count;
        }

        #region RPS calculates 

        private double CalculateRPSMO(double homeOdd, double drawOdd, double awayOdd, string result)
        {
            double homeContribution = (result == "H") ? 1 : 0;
            double drawContribution = (result == "D") ? 1 : 0;
            double awayContribution = (result == "A") ? 1 : 0;

            double firstPow = Math.Pow(1 / homeOdd - homeContribution, 2);
            double secondPow = Math.Pow(1 / drawOdd - drawContribution, 2);
            double thirdPow = Math.Pow(1 / awayOdd - awayContribution, 2);

            double resultValue = 0.5 * (firstPow + secondPow + thirdPow);
            return resultValue;
        }

        private double CalculateRPSGoals(double over25Odd, double under25Odd, string result)
        {
            double overContribution = (result == "OV") ? 1 : 0;
            double underContribution = (result == "UN") ? 1 : 0;

            double firstPow = Math.Pow(1 / over25Odd - overContribution, 2);
            double secondPow = Math.Pow(1 / under25Odd - underContribution, 2);

            double resultValue = firstPow + secondPow;
            return resultValue;
        }

        private double CalculateRPSBTTS(double bttsYesOdd, double bttsNoOdd, string result)
        {
            double overContribution = (result == "S") ? 1 : 0;
            double underContribution = (result == "N") ? 1 : 0;

            double firstPow = Math.Pow(1 / bttsYesOdd - overContribution, 2);
            double secondPow = Math.Pow(1 / bttsNoOdd - underContribution, 2);

            double resultValue = firstPow + secondPow;
            return resultValue;
        }

        #endregion

        private void UpdateTeamAverageData(List<TeamAverageData> teamsAverageData, List<TeamMatchesBySeason> teamMatches, MatchBaseData match, bool isHomeTeam)
        {
            string teamName = isHomeTeam ? match.HomeTeam : match.AwayTeam;
            TeamMatchesBySeason teamData = teamMatches
                .FirstOrDefault(tmb => tmb.TeamName == teamName && tmb.Season == match.Season);

            if (teamData == null)
            {
                teamData = new TeamMatchesBySeason
                {
                    TeamName = teamName,
                    Season = match.Season,
                    Matches = new List<MatchBaseData>()
                };
                teamMatches.Add(teamData);
            }

            teamData.Matches.Add(match);
            var matchesToAverageData = teamData.Matches.OrderBy(m => m.Date).TakeLast(10).ToList();

            var averageIndex = teamsAverageData.FindIndex(tad => tad.TeamName == teamName);

            if (averageIndex != -1)
                teamsAverageData.RemoveAt(averageIndex);

            var averageData = new TeamAverageData(teamName);
            CalculateAverage(averageData, matchesToAverageData);
            teamsAverageData.Add(averageData);
        }

        #endregion

        private MatchBarCode GetMatchBarCode(MatchBaseData match, List<TeamAverageData> teamsAverageData, int index)
        {
            MatchBarCode barCode = new MatchBarCode(
                index, match.MatchCode,
                match.HomeOdd,
                match.DrawOdd,
                match.AwayOdd,
                match.Over25Odd,
                match.Under25Odd,
                match.BttsYesOdd,
                match.BttsNoOdd,
                match.MatchResult,
                match.MatchResultHT,
                match.BttsResult,
                match.HomeGoals,
                match.AwayGoals);

            var homeAverageData = teamsAverageData.Where(t => t.TeamName == match.HomeTeam).FirstOrDefault();
            var awayAverageData = teamsAverageData.Where(t => t.TeamName == match.AwayTeam).FirstOrDefault();

            var stdMatchOdds = MathUtils.StandardDeviation(new List<double>() { match.HomeOdd, match.DrawOdd, match.AwayOdd });
            var avgMatchOdds = (match.HomeOdd + match.DrawOdd + match.AwayOdd) / 3;

            barCode.PowerPoint = (homeAverageData != null && awayAverageData != null) ? homeAverageData.HomePointsAvg - awayAverageData.AwayPointsAvg : (double?)null;
            barCode.PowerPointHT = (homeAverageData != null && awayAverageData != null) ? homeAverageData.HomePointsHTAvg - awayAverageData.AwayPointsHTAvg : (double?)null;
            barCode.CVMatchOdds = stdMatchOdds / avgMatchOdds;

            if (homeAverageData != null)
            {
                barCode.HomePoints = homeAverageData.HomePointsAvg;
                barCode.HomePointsHT = homeAverageData.HomePointsHTAvg;
                barCode.HomeCVPoints = homeAverageData.HomePointsCv;
                barCode.HomeCVPointsHT = homeAverageData.HomePointsHTCv;
                barCode.HomeDifferenceGoals = homeAverageData.HomeGoalsDifferenceAvg;
                barCode.HomeCVDifferenceGoals = homeAverageData.HomeGoalsDifferenceCv;
                barCode.HomeDifferenceGoalsHT = homeAverageData.HomeGoalsDifferenceHTAvg;
                barCode.HomeCVDifferenceGoalsHT = homeAverageData.HomeGoalsDifferenceHTCv;
                barCode.HomePoisson = MathUtils.PoissonProbability(homeAverageData.HomeGoalScoredAvg, 2);
                barCode.HomePoissonHT = MathUtils.PoissonProbability(homeAverageData.HomeGoalScoredHTAvg, 1);
                barCode.HomeGoalsScored = homeAverageData.HomeGoalScoredAvg;
                barCode.HomeGoalsScoredValue = homeAverageData.HomeGoalScoredValueAvg;
                barCode.HomeGoalsScoredCost = homeAverageData.HomeGoalScoredCostAvg;
                barCode.HomeGoalsScoredHT = homeAverageData.HomeGoalScoredHTAvg;
                barCode.HomeGoalsScoredValueHT = homeAverageData.HomeGoalScoredValueHTAvg;
                barCode.HomeGoalsScoredCostHT = homeAverageData.HomeGoalScoredCostHTAvg;
                barCode.HomeGoalsConceded = homeAverageData.HomeGoalConcededAvg;
                barCode.HomeGoalsConcededValue = homeAverageData.HomeGoalConcededValueAvg;
                barCode.HomeGoalsConcededCost = homeAverageData.HomeGoalConcededCostAvg;
                barCode.HomeGoalsConcededHT = homeAverageData.HomeGoalConcededHTAvg;
                barCode.HomeGoalsConcededValueHT = homeAverageData.HomeGoalConcededValueHTAvg;
                barCode.HomeGoalsConcededCostHT = homeAverageData.HomeGoalConcededCostHTAvg;
                barCode.HomeGoalsScoredCV = homeAverageData.HomeGoalScoredCv;
                barCode.HomeGoalsScoredCVHT = homeAverageData.HomeGoalScoredHTCv;
                barCode.HomeGoalsConcededCV = homeAverageData.HomeGoalConcededCv;
                barCode.HomeGoalsConcededCVHT = homeAverageData.HomeGoalConcededHTCv;
                barCode.HomeOddsCV = homeAverageData.HomeOddsCv;
                barCode.HomeMatchOddsRPS = homeAverageData.HomeRPSMO;
                barCode.HomeMatchOddsHTRPS = homeAverageData.HomeRPSMOHT;
                barCode.HomeGoalsRPS = homeAverageData.HomeRPSGoals;
                barCode.HomeBTTSRPS = homeAverageData.HomeRPSBTTS;
            }

            if (awayAverageData != null)
            {
                barCode.AwayPoints = awayAverageData.AwayPointsAvg;
                barCode.AwayPointsHT = awayAverageData.AwayPointsHTAvg;
                barCode.AwayCVPoints = awayAverageData.AwayPointsCv;
                barCode.AwayCVPointsHT = awayAverageData.AwayPointsHTCv;
                barCode.AwayDifferenceGoals = awayAverageData.AwayGoalsDifferenceAvg;
                barCode.AwayCVDifferenceGoals = awayAverageData.AwayGoalsDifferenceCv;
                barCode.AwayDifferenceGoalsHT = awayAverageData.AwayGoalsDifferenceHTAvg;
                barCode.AwayCVDifferenceGoalsHT = awayAverageData.AwayGoalsDifferenceHTCv;
                barCode.AwayPoisson = MathUtils.PoissonProbability(awayAverageData.AwayGoalScoredAvg, 2);
                barCode.AwayPoissonHT = MathUtils.PoissonProbability(awayAverageData.AwayGoalScoredHTAvg, 1);
                barCode.AwayGoalsScored = awayAverageData.AwayGoalScoredAvg;
                barCode.AwayGoalsScoredValue = awayAverageData.AwayGoalScoredValueAvg;
                barCode.AwayGoalsScoredCost = awayAverageData.AwayGoalScoredCostAvg;
                barCode.AwayGoalsScoredHT = awayAverageData.AwayGoalScoredHTAvg;
                barCode.AwayGoalsScoredValueHT = awayAverageData.AwayGoalScoredValueHTAvg;
                barCode.AwayGoalsScoredCostHT = awayAverageData.AwayGoalScoredCostHTAvg;
                barCode.AwayGoalsConceded = awayAverageData.AwayGoalConcededAvg;
                barCode.AwayGoalsConcededValue = awayAverageData.AwayGoalConcededValueAvg;
                barCode.AwayGoalsConcededCost = awayAverageData.AwayGoalConcededCostAvg;
                barCode.AwayGoalsConcededHT = awayAverageData.AwayGoalConcededHTAvg;
                barCode.AwayGoalsConcededValueHT = awayAverageData.AwayGoalConcededValueHTAvg;
                barCode.AwayGoalsConcededCostHT = awayAverageData.AwayGoalConcededCostHTAvg;
                barCode.AwayGoalsScoredCV = awayAverageData.AwayGoalScoredCv;
                barCode.AwayGoalsScoredCVHT = awayAverageData.AwayGoalScoredHTCv;
                barCode.AwayGoalsConcededCV = awayAverageData.AwayGoalConcededCv;
                barCode.AwayGoalsConcededCVHT = awayAverageData.AwayGoalConcededHTCv;
                barCode.AwayOddsCV = awayAverageData.AwayOddsCv;
                barCode.AwayMatchOddsRPS = awayAverageData.AwayRPSMO;
                barCode.AwayMatchOddsHTRPS = awayAverageData.AwayRPSMOHT;
                barCode.AwayGoalsRPS = awayAverageData.AwayRPSGoals;
                barCode.AwayBTTSRPS = awayAverageData.AwayRPSBTTS;
            }

            return barCode;
        }

        private List<MatchAnalyzed> CalculateKNN(List<MatchBarCode> matches, List<MatchBaseData> baseMatches)
        {
            List<MatchAnalyzed> matchesAnalyzed = new List<MatchAnalyzed>();

            for (int k = 100; k < matches.Count; k++)
            {
                KNNCalculator knnCalculator = new KNNCalculator();
                MatchBarCode matchToCalculate = matches[k];
                MatchBaseData baseMatch = baseMatches.Where(bm => bm.MatchCode == matchToCalculate.MatchCode).FirstOrDefault();

                // Começo do item 6 porque por conta da normalização, os primeiros itens estão zerados
                for (int i = 5; i < k; i++)
                {
                    MatchBarCode matchReference = matches[i];
                    knnCalculator.CalculateKNN(matchToCalculate, matchReference);
                }

                knnCalculator.GetTop3();

                string matchOddsClassification = knnCalculator.GetMatchOddsClassification();
                string matchOddsHTClassification = knnCalculator.GetMatchOddsHTClassification();
                string goalsClassification = knnCalculator.GetGoalsClassification();
                string bttsClassification = knnCalculator.GetBttsClassification();

                MatchAnalyzed matchAnalyzed = new MatchAnalyzed(baseMatch, matchOddsClassification, matchOddsHTClassification, goalsClassification, bttsClassification);
                matchesAnalyzed.Add(matchAnalyzed);
            }

            return matchesAnalyzed;
        }

        private List<Strategy> CalculateResults(List<MatchAnalyzed> matches, double stake)
        {
            List<Strategy> strategies = new List<Strategy>();
            Dictionary<string, List<MatchAnalyzed>> matchesGrouped = GetMatchesGroupedByClassification(matches);
            List<string> strategiesName = StrategyUtils.GetStrategyNames();

            foreach (var strategyName in strategiesName)
            {
                Strategy strategy = new Strategy(strategyName);

                foreach (string classification in matchesGrouped.Keys)
                {
                    StrategyClassification strategyClassification = new StrategyClassification(classification);
                    List<MatchAnalyzed> matchesByClassification = matchesGrouped.Where(mg => mg.Key == classification).FirstOrDefault().Value;
                    List<double> results = new List<double>();
                    Dictionary<string, List<double>> resultsPerSeason = StrategyUtils.GetMatchResults(matchesByClassification, strategyName, stake);

                    foreach (var kvp in resultsPerSeason)
                    {
                        List<double> seasonResults = kvp.Value;
                        results.AddRange(seasonResults);
                    }

                    Dictionary<string, double> winRatePerSeason = GetWinRatePerSeason(resultsPerSeason);
                    List<double> treatedWinRates = winRatePerSeason.Select(wrps => wrps.Value).Where(r => r > 0).ToList();
                    double stdHistoricalWinRate = MathUtils.StandardDeviation(treatedWinRates);
                    double avgHistoricalWinRate = treatedWinRates.Average();

                    double winRate = (double)results.Where(r => r > 0).Count() / (double)results.Count();

                    strategyClassification.ProfitLoss = results.Sum() / stake;
                    strategyClassification.WinRate = winRate;
                    strategyClassification.HistoricalCoefficientVariation = stdHistoricalWinRate / avgHistoricalWinRate;
                    strategyClassification.AverageOdd = 1 / winRate;
                    strategyClassification.TotalMatches = results.Count();
                    strategyClassification.TotalGreens = results.Where(r => r > 0).Count();
                    strategyClassification.TotalReds = results.Where(r => r < 0).Count();

                    strategy.StrategyClassifications.Add(strategyClassification);
                }

                strategies.Add(strategy);
            }

            return strategies;
        }

        private Dictionary<string, List<MatchAnalyzed>> GetMatchesGroupedByClassification(List<MatchAnalyzed> matches)
        {
            Dictionary<string, List<MatchAnalyzed>> matchesByClassification = new Dictionary<string, List<MatchAnalyzed>>();
            // Match Odds
            matchesByClassification.Add("BackH", new List<MatchAnalyzed>());
            matchesByClassification.Add("BackD", new List<MatchAnalyzed>());
            matchesByClassification.Add("BackA", new List<MatchAnalyzed>());
            matchesByClassification.Add("LayA", new List<MatchAnalyzed>());
            matchesByClassification.Add("LayD", new List<MatchAnalyzed>());
            matchesByClassification.Add("LayH", new List<MatchAnalyzed>());
            matchesByClassification.Add("Misto", new List<MatchAnalyzed>());

            // Match Odds HT
            matchesByClassification.Add("BackH-HT", new List<MatchAnalyzed>());
            matchesByClassification.Add("BackD-HT", new List<MatchAnalyzed>());
            matchesByClassification.Add("BackA-HT", new List<MatchAnalyzed>());
            matchesByClassification.Add("LayA-HT", new List<MatchAnalyzed>());
            matchesByClassification.Add("LayD-HT", new List<MatchAnalyzed>());
            matchesByClassification.Add("LayH-HT", new List<MatchAnalyzed>());
            matchesByClassification.Add("Misto-HT", new List<MatchAnalyzed>());

            // Goals
            matchesByClassification.Add("Over", new List<MatchAnalyzed>());
            matchesByClassification.Add("Und", new List<MatchAnalyzed>());
            matchesByClassification.Add("Sover", new List<MatchAnalyzed>());
            matchesByClassification.Add("Sund", new List<MatchAnalyzed>());

            // BTTS
            matchesByClassification.Add("Sim", new List<MatchAnalyzed>());
            matchesByClassification.Add("Não", new List<MatchAnalyzed>());
            matchesByClassification.Add("SuperS", new List<MatchAnalyzed>());
            matchesByClassification.Add("SuperN", new List<MatchAnalyzed>());

            foreach (MatchAnalyzed match in matches)
            {
                var classificationMO = matchesByClassification.Where(k => k.Key == match.MatchOddsClassification).FirstOrDefault();
                classificationMO.Value.Add(match);

                var classificationMOHT = matchesByClassification.Where(k => k.Key == match.MatchOddsHTClassification).FirstOrDefault();
                classificationMOHT.Value.Add(match);

                var classificationGoals = matchesByClassification.Where(k => k.Key == match.GoalsClassification).FirstOrDefault();
                classificationGoals.Value.Add(match);

                var classificationBtts = matchesByClassification.Where(k => k.Key == match.BttsClassification).FirstOrDefault();
                classificationBtts.Value.Add(match);
            }

            return matchesByClassification;
        }

        private Dictionary<string, double> GetWinRatePerSeason(Dictionary<string, List<double>> resultsPerSeason)
        {
            Dictionary<string, double> winRatePerSeason = new Dictionary<string, double>();

            foreach (var rps in resultsPerSeason)
            {
                var winRate = (double)rps.Value.Where(r => r > 0).Count() / (double)rps.Value.Count();
                winRatePerSeason.Add(rps.Key, winRate);
            }

            return winRatePerSeason;
        }

        private string GetClassificationGroup(string name)
        {
            if (
                name == "BackH" ||
                name == "BackD" ||
                name == "BackA" ||
                name == "LayA" ||
                name == "LayD" ||
                name == "LayH" ||
                name == "Misto"
                )
            {
                return "Grupo 1";
            }
            else if (name == "BackH-HT" ||
                name == "BackD-HT" ||
                name == "BackA-HT" ||
                name == "LayA-HT" ||
                name == "LayD-HT" ||
                name == "LayH-HT" ||
                name == "Misto-HT")
            {
                return "Grupo 2";
            }
            else if (name == "Over" || name == "Und" || name == "Sover" || name == "Sund")
                return "Grupo 3";
            else if (name == "Sim" || name == "Não" || name == "SuperS" || name == "SuperN")
                return "Grupo 4";
            else
                return "Outro";
        }

        private string GetClassificationProperty(string name)
        {
            if (
                name == "BackH" ||
                name == "BackD" ||
                name == "BackA" ||
                name == "LayA" ||
                name == "LayD" ||
                name == "LayH" ||
                name == "Misto"
                )
            {
                return "MatchOddsClassification";
            }
            else if (name == "BackH-HT" ||
                name == "BackD-HT" ||
                name == "BackA-HT" ||
                name == "LayA-HT" ||
                name == "LayD-HT" ||
                name == "LayH-HT" ||
                name == "Misto-HT")
            {
                return "MatchOddsHTClassification";
            }
            else if (name == "Over" || name == "Und" || name == "Sover" || name == "Sund")
                return "GoalsClassification";
            else if (name == "Sim" || name == "Não" || name == "SuperS" || name == "SuperN")
                return "BttsClassification";
            else
                return "Outro";
        }

        private StrategyInfo FilterMatchesByClassifications(Strategy strategy, List<MatchAnalyzed> matches, double stake)
        {
            List<MatchAnalyzed> matchesWithValue = new List<MatchAnalyzed>();
            List<MatchAnalyzed> filteredMatches = new List<MatchAnalyzed>();
            List<string> classificationsApplied = new List<string>();

            var groupedClassifications = strategy.StrategyClassifications
                .GroupBy(sc => GetClassificationGroup(sc.Name)).ToList();

            List<string> groupPriority = StrategyUtils.GetPriorityRankingGroupByStrategy(strategy.Name);

            double lastResult = 0;

            foreach (string priority in groupPriority)
            {
                List<StrategyClassification> classifications = groupedClassifications
                    .Where(gc => gc.Key == priority)
                    .SelectMany(gc => gc.ToList())
                    .ToList();

                foreach (StrategyClassification classification in classifications)
                {
                    string propertyClassification = GetClassificationProperty(classification.Name);
                    List<MatchAnalyzed> matchesFilteredByClassification = matches.Where(m => GetClassificationPropertyValue(m, propertyClassification) == classification.Name).ToList();

                    filteredMatches.AddRange(matchesFilteredByClassification);
                    filteredMatches = filteredMatches.DistinctBy(fm => fm.MatchCode).ToList();

                    Dictionary<string, List<double>> resultDictionary = StrategyUtils.GetMatchResults(filteredMatches, strategy.Name, stake);
                    double result = resultDictionary.Values.SelectMany(list => list).Sum() / stake;

                    if (result > lastResult)
                    {
                        matchesWithValue = filteredMatches;
                        lastResult = result;
                        classificationsApplied.Add(classification.Name);
                    }
                }

            }

            StrategyInfo strategyInfo = null;

            if (matchesWithValue.Count > matches.Count * 0.25)
                strategyInfo = new StrategyInfo(strategy.Name, classificationsApplied, matchesWithValue, lastResult);
            
            return strategyInfo;
        }

        private string GetClassificationPropertyValue(MatchAnalyzed obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
                return propertyInfo.GetValue(obj).ToString();

            return null;
        }

        #endregion
    }
}

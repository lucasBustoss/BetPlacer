using BetPlacer.Punter.API.Models;
using BetPlacer.Punter.API.Models.Match.Team;
using BetPlacer.Punter.API.Utils;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BetPlacer.Punter.API.Services
{
    public class CalculateStatsService
    {
        public void CalculateStats(List<MatchBaseData> matchBaseData)
        {
            List<TeamAverageData> teamsAverageData = new List<TeamAverageData>();
            
            List<TeamMatchesBySeason> teamMatchesBySeason = matchBaseData
                .SelectMany(m => new[]
                {
                    new { TeamName = m.HomeTeam, Season = m.Season, Match = m },
                    new { TeamName = m.AwayTeam, Season = m.Season, Match = m }
                })
                .GroupBy(t => new { t.TeamName, t.Season })
                .Select(g => new TeamMatchesBySeason
                {
                    TeamName = g.Key.TeamName,
                    Season = g.Key.Season,
                    Matches = g.Select(x => x.Match).ToList()
                })
                .ToList();

            foreach (TeamMatchesBySeason teamSeason in teamMatchesBySeason)
            {
                if (teamSeason.Season != "2013")
                    continue;
                
                TeamAverageData averageData = new TeamAverageData(teamSeason.TeamName, teamSeason.Season);
                CalculateAverage(averageData, teamSeason.Matches);

                teamsAverageData.Add(averageData);
            }
            var teamsSorted = teamsAverageData.OrderBy(x => x.TeamName).ToList();

            Console.WriteLine(teamsSorted);
        }

        #region Private methods

        private void CalculateAverage(TeamAverageData averageData, List<MatchBaseData> matches)
        {
            var last10MatchesHome = matches.Where(m => m.HomeTeam == averageData.TeamName).ToList();
            var last10MatchesAway = matches.Where(m => m.AwayTeam == averageData.TeamName).ToList();

            CalculateScoredAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculateConcededAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculatePointsAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculateDifferenceGoalsAverage(averageData, last10MatchesHome, last10MatchesAway);
            CalculateOddsAverage(averageData, last10MatchesHome, last10MatchesAway);
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

            double avgHomeGoalScored = sumHomeScoredGoalsValue / sumAwayPercentageOdd;
            double avgHomeGoalScoredValue = sumHomeScoredGoalsValue / homeMatches.Count();
            double avgHomeGoalScoredCost = sumHomeScoredGoalsCost / homeMatches.Count();

            averageData.HomeGoalScoredAvg = Math.Round(avgHomeGoalScored, 2);
            averageData.HomeGoalScoredStd = Math.Round(stdHomeGoalScored, 2);
            averageData.HomeGoalScoredCv = Math.Round(stdHomeGoalScored / avgHomeGoalScored, 2);
            averageData.HomeGoalScoredValueAvg = Math.Round(avgHomeGoalScoredValue, 2);
            averageData.HomeGoalScoredCostAvg = Math.Round(avgHomeGoalScoredCost, 2);

            #endregion

            #region Away

            double sumAwayScoredGoalsValue = awayMatches.Sum(ma => ma.AwayScoredGoalValue);
            double sumAwayScoredGoalsCost = awayMatches.Sum(ma => ma.AwayScoredGoalCost);

            double stdAwayGoalScored = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoals).ToList());

            double avgAwayGoalScored = sumAwayScoredGoalsValue / sumHomePercentageOdd;
            double avgAwayGoalScoredValue = sumAwayScoredGoalsValue / awayMatches.Count();
            double avgAwayGoalScoredCost = sumAwayScoredGoalsCost / awayMatches.Count();

            averageData.AwayGoalScoredAvg = Math.Round(avgAwayGoalScored, 2);
            averageData.AwayGoalScoredStd = Math.Round(stdAwayGoalScored, 2);
            averageData.AwayGoalScoredCv = Math.Round(stdAwayGoalScored / avgAwayGoalScored, 2);
            averageData.AwayGoalScoredValueAvg = Math.Round(avgAwayGoalScoredValue, 2);
            averageData.AwayGoalScoredCostAvg = Math.Round(avgAwayGoalScoredCost, 2);

            #endregion

            #endregion

            #region HT

            #region Home

            double sumHomeScoredGoalsValueHT = homeMatches.Sum(mh => mh.HomeScoredGoalValueHT);
            double sumHomeScoredGoalsCostHT = homeMatches.Sum(mh => mh.HomeScoredGoalCostHT);

            double stdHomeGoalScoredHT = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.HomeGoalsHT).ToList());

            double avgHomeGoalScoredHT = sumHomeScoredGoalsValueHT / sumAwayPercentageOdd;
            double avgHomeGoalScoredValueHT = sumHomeScoredGoalsValueHT / homeMatches.Count();
            double avgHomeGoalScoredCostHT = sumHomeScoredGoalsCostHT / homeMatches.Count();

            averageData.HomeGoalScoredHTAvg = Math.Round(avgHomeGoalScoredHT, 2);
            averageData.HomeGoalScoredHTStd = Math.Round(stdHomeGoalScoredHT, 2);
            averageData.HomeGoalScoredHTCv = Math.Round(stdHomeGoalScoredHT / avgHomeGoalScoredHT, 2);
            averageData.HomeGoalScoredValueHTAvg = Math.Round(avgHomeGoalScoredValueHT, 2);
            averageData.HomeGoalScoredCostHTAvg = Math.Round(avgHomeGoalScoredCostHT, 2);

            #endregion

            #region Away

            double sumAwayScoredGoalsValueHT = awayMatches.Sum(ma => ma.AwayScoredGoalValueHT);
            double sumAwayScoredGoalsCostHT = awayMatches.Sum(ma => ma.AwayScoredGoalCostHT);

            double stdAwayGoalScoredHT = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoalsHT).ToList());

            double avgAwayGoalScoredHT = sumAwayScoredGoalsValueHT / sumHomePercentageOdd;
            double avgAwayGoalScoredValueHT = sumAwayScoredGoalsValueHT / awayMatches.Count();
            double avgAwayGoalScoredCostHT = sumAwayScoredGoalsCostHT / awayMatches.Count();

            averageData.AwayGoalScoredHTAvg = Math.Round(avgAwayGoalScoredHT, 2);
            averageData.AwayGoalScoredHTStd = Math.Round(stdAwayGoalScoredHT, 2);
            averageData.AwayGoalScoredHTCv = Math.Round(stdAwayGoalScoredHT / avgAwayGoalScoredHT, 2);
            averageData.AwayGoalScoredValueHTAvg = Math.Round(avgAwayGoalScoredValueHT, 2);
            averageData.AwayGoalScoredCostHTAvg = Math.Round(avgAwayGoalScoredCostHT, 2);

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

            double avgHomeGoalConcededValue = sumHomeConcededGoalsValue / sumAwayPercentageOdd;

            averageData.HomeGoalConcededAvg = Math.Round(avgHomeGoalConcededValue, 2);
            averageData.HomeGoalConcededStd = Math.Round(stdHomeGoalConcededValue, 2);
            averageData.HomeGoalConcededCv = Math.Round(stdHomeGoalConcededValue / avgHomeGoalConcededValue, 2);
            averageData.HomeGoalConcededValueAvg = Math.Round(sumHomeConcededGoalsValue / homeMatches.Count(), 2);
            averageData.HomeGoalConcededCostAvg = Math.Round(sumHomeConcededGoalsCost / homeMatches.Count(), 2);

            #endregion

            #region Away

            double sumAwayConcededGoalsValue = awayMatches.Sum(mh => mh.AwayConcededGoalValue);
            double sumAwayConcededGoalsCost = awayMatches.Sum(mh => mh.AwayConcededGoalCost);
            double sumHomePercentageOdd = awayMatches.Sum(mh => mh.HomePercentageOdd);

            double stdAwayGoalConcededValue = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.HomeGoals).ToList());

            double avgAwayGoalConcededValue = sumAwayConcededGoalsValue / sumHomePercentageOdd;

            averageData.AwayGoalConcededAvg = Math.Round(avgAwayGoalConcededValue, 2);
            averageData.AwayGoalConcededStd = Math.Round(stdAwayGoalConcededValue, 2);
            averageData.AwayGoalConcededCv = Math.Round(stdAwayGoalConcededValue / avgAwayGoalConcededValue, 2);
            averageData.AwayGoalConcededValueAvg = Math.Round(sumAwayConcededGoalsValue / awayMatches.Count(), 2);
            averageData.AwayGoalConcededCostAvg = Math.Round(sumAwayConcededGoalsCost / awayMatches.Count(), 2);

            #endregion

            #endregion

            #region HT

            #region Home

            double sumHomeConcededGoalsValueHT = homeMatches.Sum(mh => mh.HomeConcededGoalValueHT);
            double sumHomeConcededGoalsCostHT = homeMatches.Sum(mh => mh.HomeConcededGoalCostHT);
            double sumAwayPercentageOddHT = homeMatches.Sum(mh => mh.AwayPercentageOdd);

            double stdHomeGoalConcededValueHT = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.AwayGoalsHT).ToList());

            double avgHomeGoalConcededValueHT = sumHomeConcededGoalsValueHT / sumAwayPercentageOdd;

            averageData.HomeGoalConcededHTAvg = Math.Round(avgHomeGoalConcededValueHT, 2);
            averageData.HomeGoalConcededHTStd = Math.Round(stdHomeGoalConcededValueHT, 2);
            averageData.HomeGoalConcededHTCv = Math.Round(stdHomeGoalConcededValueHT / avgHomeGoalConcededValueHT, 2);
            averageData.HomeGoalConcededValueHTAvg = Math.Round(sumHomeConcededGoalsValueHT / homeMatches.Count(), 2);
            averageData.HomeGoalConcededCostHTAvg = Math.Round(sumHomeConcededGoalsCostHT / homeMatches.Count(), 2);

            #endregion

            #region Away

            double sumAwayConcededGoalsValueHT = awayMatches.Sum(mh => mh.AwayConcededGoalValueHT);
            double sumAwayConcededGoalsCostHT = awayMatches.Sum(mh => mh.AwayConcededGoalCostHT);
            double sumHomePercentageOddHT = awayMatches.Sum(mh => mh.HomePercentageOdd);

            double stdAwayGoalConcededValueHT = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.HomeGoalsHT).ToList());

            double avgAwayGoalConcededValueHT = sumAwayConcededGoalsValueHT / sumHomePercentageOdd;

            averageData.AwayGoalConcededHTAvg = Math.Round(avgAwayGoalConcededValueHT, 2);
            averageData.AwayGoalConcededHTStd = Math.Round(stdAwayGoalConcededValueHT, 2);
            averageData.AwayGoalConcededHTCv = Math.Round(stdAwayGoalConcededValueHT / avgAwayGoalConcededValueHT, 2);
            averageData.AwayGoalConcededValueHTAvg = Math.Round(sumAwayConcededGoalsValueHT / awayMatches.Count(), 2);
            averageData.AwayGoalConcededCostHTAvg = Math.Round(sumAwayConcededGoalsCostHT / awayMatches.Count(), 2);

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

            double avgHomePointsValue = sumHomePointsValue / sumAwayPercentageOdd;

            averageData.HomePointsAvg = Math.Round(avgHomePointsValue, 2);
            averageData.HomePointsStd = Math.Round(stdHomePoints, 2);
            averageData.HomePointsCv = Math.Round(stdHomePoints / avgHomePointsValue, 2);

            #endregion

            #region Away

            double sumAwayPointsValue = awayMatches.Sum(am => am.AwayPointsValue);

            double stdAwayPoints = MathUtils.StandardDeviation(awayMatches.Select(am => (double)am.AwayPoints).ToList());

            double avgAwayPointsValue = sumAwayPointsValue / sumHomePercentageOdd;

            averageData.AwayPointsAvg = Math.Round(avgAwayPointsValue, 2);
            averageData.AwayPointsStd = Math.Round(stdAwayPoints, 2);
            averageData.AwayPointsCv = Math.Round(stdAwayPoints / avgAwayPointsValue, 2);

            #endregion

            #endregion

            #region HT

            #region Home

            double sumHomePointsValueHT = homeMatches.Sum(hm => hm.HomePointsValueHT);

            double stdHomePointsHT = MathUtils.StandardDeviation(homeMatches.Select(hm => (double)hm.HomePointsHT).ToList());

            double avgHomePointsValueHT = sumHomePointsValueHT / sumAwayPercentageOdd;

            averageData.HomePointsHTAvg = Math.Round(avgHomePointsValueHT, 2);
            averageData.HomePointsHTStd = Math.Round(stdHomePointsHT, 2);
            averageData.HomePointsHTCv = Math.Round(stdHomePointsHT / avgHomePointsValueHT, 2);

            #endregion

            #region Away

            double sumAwayPointsValueHT = awayMatches.Sum(am => am.AwayPointsValueHT);

            double stdAwayPointsHT = MathUtils.StandardDeviation(awayMatches.Select(am => (double)am.AwayPointsHT).ToList());

            double avgAwayPointsValueHT = sumAwayPointsValueHT / sumHomePercentageOdd;

            averageData.AwayPointsHTAvg = Math.Round(avgAwayPointsValueHT, 2);
            averageData.AwayPointsHTStd = Math.Round(stdAwayPointsHT, 2);
            averageData.AwayPointsHTCv = Math.Round(stdAwayPointsHT / avgAwayPointsValueHT, 2);

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

            double avgHomeGoalDifferenceValue = sumHomeGoalsDifferenceValue / sumAwayPercentageOdd;

            averageData.HomeGoalsDifferenceAvg = Math.Round(avgHomeGoalDifferenceValue, 2);
            averageData.HomeGoalsDifferenceStd = Math.Round(stdHomeGoalsDifferenceValue, 2);
            averageData.HomeGoalsDifferenceCv = Math.Abs(Math.Round(stdHomeGoalsDifferenceValue / avgHomeGoalDifferenceValue, 2));

            #endregion

            #region Away

            double sumAwayGoalsDifferenceValue = awayMatches.Sum(mh => mh.AwayGoalsDifferenceValue);

            double stdAwayGoalsDifferenceValue = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoalsDifference).ToList());

            double avgAwayGoalDifferenceValue = sumAwayGoalsDifferenceValue / sumHomePercentageOdd;

            averageData.AwayGoalsDifferenceAvg = Math.Round(avgAwayGoalDifferenceValue, 2);
            averageData.AwayGoalsDifferenceStd = Math.Round(stdAwayGoalsDifferenceValue, 2);
            averageData.AwayGoalsDifferenceCv = Math.Abs(Math.Round(stdAwayGoalsDifferenceValue / avgAwayGoalDifferenceValue, 2));

            #endregion

            #endregion

            #region HT

            #region Home

            double sumHomeGoalsDifferenceValueHT = homeMatches.Sum(mh => mh.HomeGoalsDifferenceValueHT);

            double stdHomeGoalsDifferenceValueHT = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.HomeGoalsDifferenceHT).ToList());

            double avgHomeGoalDifferenceValueHT = sumHomeGoalsDifferenceValueHT / sumAwayPercentageOdd;

            averageData.HomeGoalsDifferenceHTAvg = Math.Round(avgHomeGoalDifferenceValueHT, 2);
            averageData.HomeGoalsDifferenceHTStd = Math.Round(stdHomeGoalsDifferenceValueHT, 2);
            averageData.HomeGoalsDifferenceHTCv = Math.Abs(Math.Round(stdHomeGoalsDifferenceValueHT / avgHomeGoalDifferenceValueHT, 2));

            #endregion

            #region Away

            double sumAwayGoalsDifferenceValueHT = awayMatches.Sum(mh => mh.AwayGoalsDifferenceValueHT);

            double stdAwayGoalsDifferenceValueHT = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoalsDifferenceHT).ToList());

            double avgAwayGoalDifferenceValueHT = sumAwayGoalsDifferenceValueHT / sumHomePercentageOdd;

            averageData.AwayGoalsDifferenceHTAvg = Math.Round(avgAwayGoalDifferenceValueHT, 2);
            averageData.AwayGoalsDifferenceHTStd = Math.Round(stdAwayGoalsDifferenceValueHT, 2);
            averageData.AwayGoalsDifferenceHTCv = Math.Abs(Math.Round(stdAwayGoalsDifferenceValueHT / avgAwayGoalDifferenceValueHT, 2));

            #endregion

            #endregion
        }

        private void CalculateOddsAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            #region Home

            var sumHomeOdds = homeMatches.Sum(hm => hm.HomeOdd);

            var stdHomeOdds = MathUtils.StandardDeviation(homeMatches.Select(hm => hm.HomeOdd).ToList());

            var avgHomeOdds = sumHomeOdds / homeMatches.Count();

            averageData.HomeOddsAvg = Math.Round(avgHomeOdds, 2);
            averageData.HomeOddsStd = Math.Round(stdHomeOdds, 2);
            averageData.HomeOddsCv = Math.Round(stdHomeOdds / avgHomeOdds, 2);

            #endregion

            #region Away

            var sumAwayOdds = awayMatches.Sum(hm => hm.AwayOdd);

            var stdAwayOdds = MathUtils.StandardDeviation(awayMatches.Select(hm => hm.AwayOdd).ToList());

            var avgAwayOdds = sumAwayOdds / awayMatches.Count();

            averageData.AwayOddsAvg = Math.Round(avgAwayOdds, 2);
            averageData.AwayOddsStd = Math.Round(stdAwayOdds, 2);
            averageData.AwayOddsCv = Math.Round(stdAwayOdds / avgAwayOdds, 2);

            #endregion
        }

        #endregion
    }
}

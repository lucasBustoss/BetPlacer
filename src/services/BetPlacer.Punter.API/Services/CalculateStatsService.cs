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
        }

        private void CalculateScoredAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            #region Home

            double sumHomeScoredGoalsValue = homeMatches.Sum(mh => mh.HomeScoredGoalValue);
            double sumHomeScoredGoalsCost = homeMatches.Sum(mh => mh.HomeScoredGoalCost);
            double sumAwayPercentageOdd = homeMatches.Sum(mh => mh.AwayPercentageOdd);

            double stdHomeGoalScoredStd = MathUtils.StandardDeviation(homeMatches.Select(mh => (double)mh.HomeGoals).ToList());

            double avgHomeGoalScored = sumHomeScoredGoalsValue / sumAwayPercentageOdd;
            double avgHomeGoalScoredValue = sumHomeScoredGoalsValue / homeMatches.Count();
            double avgHomeGoalScoredCost = sumHomeScoredGoalsCost / homeMatches.Count();

            averageData.HomeGoalScoredAvg = Math.Round(avgHomeGoalScored, 2);
            averageData.HomeGoalScoredStd = Math.Round(stdHomeGoalScoredStd, 2);
            averageData.HomeGoalScoredCv = Math.Round(stdHomeGoalScoredStd / avgHomeGoalScored, 2);
            averageData.HomeGoalScoredValueAvg = Math.Round(avgHomeGoalScoredValue, 2);
            averageData.HomeGoalScoredCostAvg = Math.Round(avgHomeGoalScoredCost, 2);

            #endregion

            #region Away

            double sumAwayScoredGoalsValue = awayMatches.Sum(ma => ma.AwayScoredGoalValue);
            double sumAwayScoredGoalsCost = awayMatches.Sum(ma => ma.AwayScoredGoalCost);
            double sumHomePercentageOdd = awayMatches.Sum(ma => ma.HomePercentageOdd);

            double stdAwayGoalScoredStd = MathUtils.StandardDeviation(awayMatches.Select(mh => (double)mh.AwayGoals).ToList());

            double avgAwayGoalScored = sumAwayScoredGoalsValue / sumHomePercentageOdd;
            double avgAwayGoalScoredValue = sumAwayScoredGoalsValue / awayMatches.Count();
            double avgAwayGoalScoredCost = sumAwayScoredGoalsCost / awayMatches.Count();

            averageData.AwayGoalScoredAvg = Math.Round(avgAwayGoalScored, 2);
            averageData.AwayGoalScoredStd = Math.Round(stdAwayGoalScoredStd, 2);
            averageData.AwayGoalScoredCv = Math.Round(stdAwayGoalScoredStd / avgAwayGoalScored, 2);
            averageData.AwayGoalScoredValueAvg = Math.Round(avgAwayGoalScoredValue, 2);
            averageData.AwayGoalScoredCostAvg = Math.Round(avgAwayGoalScoredCost, 2);

            #endregion
        }

        private void CalculateConcededAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
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
        }

        private void CalculatePointsAverage(TeamAverageData averageData, List<MatchBaseData> homeMatches, List<MatchBaseData> awayMatches)
        {
            #region Home

            double sumHomePointsValue = homeMatches.Sum(hm => hm.HomePointsValue);
            double sumAwayPercentageOdd = homeMatches.Sum(hm => hm.AwayPercentageOdd);

            double stdHomePoints = MathUtils.StandardDeviation(homeMatches.Select(hm => (double)hm.HomePoints).ToList());

            double avgHomePointsValue = sumHomePointsValue / sumAwayPercentageOdd;

            averageData.HomePointsAverage = Math.Round(avgHomePointsValue, 2);
            averageData.HomePointsStd = Math.Round(stdHomePoints, 2);
            averageData.HomePointsCv = Math.Round(stdHomePoints / avgHomePointsValue, 2);

            #endregion

            #region Away

            double sumAwayPointsValue = awayMatches.Sum(am => am.AwayPointsValue);
            double sumHomePercentageOdd = awayMatches.Sum(am => am.HomePercentageOdd);

            double stdAwayPoints = MathUtils.StandardDeviation(awayMatches.Select(am => (double)am.AwayPoints).ToList());

            double avgAwayPointsValue = sumAwayPointsValue / sumHomePercentageOdd;

            averageData.AwayPointsAverage = Math.Round(avgAwayPointsValue, 2);
            averageData.AwayPointsStd = Math.Round(stdAwayPoints, 2);
            averageData.AwayPointsCv = Math.Round(stdAwayPoints / avgAwayPointsValue, 2);

            #endregion
        }

        #endregion
    }
}

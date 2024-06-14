using BetPlacer.Punter.API.Models;
using BetPlacer.Punter.API.Models.Match.Team;
using BetPlacer.Punter.API.Utils;
using System.Collections.Generic;

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

            #region Home
            
            double sumHomeScoredGoalsValue = last10MatchesHome.Sum(mh => mh.HomeScoredGoalValue);
            double sumAwayPercentageOdd = last10MatchesHome.Sum(mh => mh.AwayPercentageOdd);
            double stdHomeGoalScoredStd = MathUtils.StandardDeviation(last10MatchesHome.Select(mh => (double)mh.HomeGoals).ToList());
            double avgHomeGoalScored = sumHomeScoredGoalsValue / sumAwayPercentageOdd;

            averageData.HomeGoalScoredAvg = Math.Round(avgHomeGoalScored, 2);
            averageData.HomeGoalScoredStd = Math.Round(stdHomeGoalScoredStd, 2);
            averageData.HomeGoalScoredCv = Math.Round(stdHomeGoalScoredStd / avgHomeGoalScored, 2);

            #endregion

            #region Away

            double sumAwayScoredGoalsValue = last10MatchesAway.Sum(mh => mh.AwayScoredGoalValue);
            double sumHomePercentageOdd = last10MatchesAway.Sum(mh => mh.HomePercentageOdd);
            double stdAwayGoalScoredStd = MathUtils.StandardDeviation(last10MatchesAway.Select(mh => (double)mh.AwayGoals).ToList());
            double avgAwayGoalScored = sumAwayScoredGoalsValue / sumHomePercentageOdd;

            averageData.AwayGoalScoredAvg = Math.Round(avgAwayGoalScored, 2);
            averageData.AwayGoalScoredStd = Math.Round(stdAwayGoalScoredStd, 2);
            averageData.AwayGoalScoredCv = Math.Round(stdAwayGoalScoredStd / avgAwayGoalScored, 2);

            #endregion
        }

        #endregion
    }
}

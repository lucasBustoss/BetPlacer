using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.Entities.Trade;
using BetPlacer.Fixtures.API.Models.Enums;
using BetPlacer.Fixtures.API.Models.ValueObjects;

namespace BetPlacer.Fixtures.API.Services
{
    public class CalculateFixtureStatsService
    {
        public List<FixtureStatsTradeModel> Calculate(IEnumerable<FixtureModel> fixtures)
        {
            List<FixtureStatsTradeModel> statsList = new List<FixtureStatsTradeModel>();

            foreach (FixtureModel fixture in fixtures.ToList())
            {
                List<FixtureModel> homePastFixturesAtHome =
                        fixtures
                        .Where(f => f.HomeTeamId == fixture.HomeTeamId && f.StartDate < fixture.StartDate)
                        .Distinct()
                        .ToList();

                List<FixtureModel> homePastFixturesAtAway =
                        fixtures
                        .Where(f => f.AwayTeamId == fixture.HomeTeamId && f.StartDate < fixture.StartDate)
                        .Distinct()
                        .ToList();

                List<FixtureModel> awayPastFixturesAtHome =
                        fixtures
                        .Where(f => f.HomeTeamId == fixture.AwayTeamId && f.StartDate < fixture.StartDate)
                        .Distinct()
                        .ToList();

                List<FixtureModel> awayPastFixturesAtAway =
                        fixtures
                        .Where(f => f.AwayTeamId == fixture.AwayTeamId && f.StartDate < fixture.StartDate)
                        .Distinct()
                        .ToList();

                FixtureStatsTradeModel stats = new FixtureStatsTradeModel();
                stats.FixtureCode = fixture.Code;

                GetPPG(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway);
                GetTotalWins(stats, fixture, homePastFixturesAtHome, homePastFixturesAtAway, awayPastFixturesAtHome, awayPastFixturesAtAway);

                statsList.Add(stats);
            }

            return statsList;
        }

        private void GetPPG(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway)
        {
            stats.HomePPGTotalAtHome = CalculatePPG(homePastFixturesAtHome, fixture.HomeTeamId);
            stats.HomePPGTotal = CalculatePPG(homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList(), fixture.HomeTeamId);
            stats.AwayPPGTotalAtAway = CalculatePPG(awayPastFixturesAtAway, fixture.AwayTeamId);
            stats.AwayPPGTotal = CalculatePPG(awayPastFixturesAtAway.Concat(awayPastFixturesAtHome).ToList(), fixture.AwayTeamId);
        }

        private void GetTotalWins(FixtureStatsTradeModel stats, FixtureModel fixture, List<FixtureModel> homePastFixturesAtHome, List<FixtureModel> homePastFixturesAtAway, List<FixtureModel> awayPastFixturesAtHome, List<FixtureModel> awayPastFixturesAtAway)
        {
            Tuple<int, double> homeTotalWins = CalculateTotalWinsAndPercentWins(homePastFixturesAtHome.Concat(homePastFixturesAtAway).ToList(), fixture.HomeTeamId);
            Tuple<int, double> homeTotalWinsAtHome = CalculateTotalWinsAndPercentWins(homePastFixturesAtHome, fixture.HomeTeamId);
            Tuple<int, double> awayTotalWins = CalculateTotalWinsAndPercentWins(awayPastFixturesAtAway.Concat(awayPastFixturesAtHome).ToList(), fixture.AwayTeamId);
            Tuple<int, double> awayTotalWinsAtAway = CalculateTotalWinsAndPercentWins(awayPastFixturesAtAway, fixture.AwayTeamId);


            stats.HomeWinsTotal = homeTotalWins.Item1;
            stats.HomeWinsPercentTotal = homeTotalWins.Item2;
            stats.HomeWinsTotalAtHome = homeTotalWinsAtHome.Item1;
            stats.HomeWinsPercentTotalAtHome = homeTotalWinsAtHome.Item2;

            stats.AwayWinsTotal = awayTotalWins.Item1;
            stats.AwayWinsPercentTotal = awayTotalWins.Item2;
            stats.AwayWinsTotalAtAway = awayTotalWinsAtAway.Item1;
            stats.AwayWinsPercentTotalAtAway = awayTotalWinsAtAway.Item2;
        }

        #region Calculations

        // Preciso verificar se é só home/away ou overall
        private double CalculatePPG(List<FixtureModel> fixtures, int teamId)
        {
            int points = 0;
            int countFixtures = 0;

            foreach (FixtureModel fixture in fixtures)
            {
                bool isHome = fixture.HomeTeamId == teamId;

                if (isHome)
                {
                    points +=
                        fixture.HomeTeamGoals > fixture.AwayTeamGoals ? 3 :
                        fixture.HomeTeamGoals == fixture.AwayTeamGoals ? 1 :
                        0;
                }
                else
                {
                    points +=
                        fixture.AwayTeamGoals > fixture.HomeTeamGoals ? 3 :
                        fixture.AwayTeamGoals == fixture.HomeTeamGoals ? 1 :
                        0;
                }

                countFixtures++;
            }

            double ppg = countFixtures > 0 ? (double)points / countFixtures : 0;
            return Math.Round(ppg, 2);
        }

        private Tuple<int, double> CalculateTotalWinsAndPercentWins(List<FixtureModel> fixtures, int teamId)
        {
            int wins = 0;
            int countFixtures = 0;

            foreach (FixtureModel fixture in fixtures)
            {
                bool isHome = fixture.HomeTeamId == teamId;

                if (isHome)
                    wins += fixture.HomeTeamGoals > fixture.AwayTeamGoals ? 1 : 0;
                else
                    wins += fixture.AwayTeamGoals > fixture.HomeTeamGoals ? 1 : 0;

                countFixtures++;
            }

            double percentWins = countFixtures > 0 ? (double)wins / countFixtures : 0;
            return new Tuple<int, double>(wins, Math.Round(percentWins, 2));
        }
        #endregion
    }
}

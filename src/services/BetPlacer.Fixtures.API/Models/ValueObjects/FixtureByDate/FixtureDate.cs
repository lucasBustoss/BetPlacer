using BetPlacer.Backtest.API.Models;
using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.Entities.Trade;

namespace BetPlacer.Fixtures.API.Models.ValueObjects.FixtureByDate
{
    public class FixtureDate
    {
        public FixtureDate(FixtureModel fixtureModel, FixtureStatsTradeModel stats, string filters)
        {
            Code = fixtureModel.Code;
            Date = fixtureModel.StartDate.AddHours(-3).ToString("dd/MM/yyyy HH:mm");
            HomeTeamName = fixtureModel.HomeTeamName;
            AwayTeamName = fixtureModel.AwayTeamName;
            OddHome = 2;
            OddDraw = 2;
            OddAway = 2;

            Filters = filters;

            if (stats != null)
            {
                Stats = stats;
                Stats.Fixture = null;
            }
        }

        public int Code { get; set; }
        public string Date { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public double OddHome { get; set; }
        public double OddDraw { get; set; }
        public double OddAway { get; set; }
        public string Filters { get; set; }
        public FixtureStatsTradeModel Stats { get; set; }
    }
}

using BetPlacer.Backtest.API.Models;
using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.Entities.Trade;

namespace BetPlacer.Fixtures.API.Models.ValueObjects.FixtureByDate
{
    public class FixtureDate
    {
        public FixtureDate(FixtureModel fixtureModel, FixtureStatsTradeModel stats, string filters, FixtureOdds odd)
        {
            Code = fixtureModel.Code;
            Date = fixtureModel.StartDate.AddHours(-3).ToString("dd/MM/yyyy HH:mm");
            HomeTeamName = fixtureModel.HomeTeamName;
            AwayTeamName = fixtureModel.AwayTeamName;
            FixtureOdds = odd;
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
        public string Filters { get; set; }
        public FixtureOdds FixtureOdds { get; set; }

        public bool InformedOdds
        {
            get
            {
                return 
                    FixtureOdds != null && 
                    FixtureOdds.HomeOdd > 0 && 
                    FixtureOdds.DrawOdd > 0 && 
                    FixtureOdds.AwayOdd > 0 && 
                    FixtureOdds.Over25Odd > 0 && 
                    FixtureOdds.Under25Odd > 0 &&
                    FixtureOdds.BTTSYesOdd > 0 &&
                    FixtureOdds.BTTSNoOdd > 0;
            }
        }

        public FixtureStatsTradeModel Stats { get; set; }
    }
}

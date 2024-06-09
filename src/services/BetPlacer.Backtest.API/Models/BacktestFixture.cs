using BetPlacer.Backtest.API.Models.Entities;

namespace BetPlacer.Backtest.API.Models
{
    public class BacktestFixture
    {
        public BacktestFixture(BacktestModel backtest, int fixtureCode)
        {
            FixtureCode = fixtureCode;
            BacktestName = backtest.Name;
            BacktestCode = backtest.Code;
        }

        public int FixtureCode { get; set; }
        public string BacktestName { get; set; }
        public int BacktestCode { get; set; }
    }
}

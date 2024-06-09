using BetPlacer.Backtest.API.Models.Entities;

namespace BetPlacer.Backtest.API.Repositories
{
    public interface IBacktestRepository
    {
        List<BacktestModel> GetBacktests(bool onlyWithFilterFixture, int id = 0);
        Task CreateBacktest(BacktestModel backtest);
    }
}

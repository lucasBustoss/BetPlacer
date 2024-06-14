using BetPlacer.Backtest.API.Models.ValueObjects;

namespace BetPlacer.Backtest.API.Repositories
{
    public interface IBacktestRepository
    {
        List<BacktestVO> GetBacktests(bool onlyWithFilterFixture, int id = 0);
        Task CreateBacktest(BacktestVO backtest);
    }
}

using BetPlacer.Backtest.API.Models.Entities;

namespace BetPlacer.Backtest.API.Repositories
{
    public interface IBacktestRepository
    {
        Task CreateBacktest(BacktestModel backtest);
    }
}

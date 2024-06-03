using BetPlacer.Backtest.API.Config;
using BetPlacer.Backtest.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetPlacer.Backtest.API.Repositories
{
    public class BacktestRepository : IBacktestRepository
    {
        private readonly BacktestDbContext _context;

        public BacktestRepository(DbContextOptions<BacktestDbContext> db)
        {
            _context = new BacktestDbContext(db);
        }

        public async Task CreateBacktest(BacktestModel backtest)
        {
            _context.Backtest.Add(backtest);

            foreach (var filter in backtest.Filters)
            {
                filter.BacktestCode = backtest.Code;
                _context.BacktestFilters.Add(filter);
            }

            foreach (var league in backtest.Leagues)
            {
                league.BacktestCode = backtest.Code;
                _context.BacktestLeaguesList.Add(league);
            }

            foreach (var leagueSeason in backtest.LeagueSeasons)
            {
                leagueSeason.BacktestCode = backtest.Code;
                _context.BacktestLeagueSeasonsList.Add(leagueSeason);
            }

            foreach (var team in backtest.Teams)
            {
                team.BacktestCode = backtest.Code;
                _context.BacktestTeamsList.Add(team);
            }

            await _context.SaveChangesAsync();
        }
    }
}

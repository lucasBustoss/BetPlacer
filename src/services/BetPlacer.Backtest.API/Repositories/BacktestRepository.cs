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
            await _context.SaveChangesAsync();
        }

        public List<BacktestModel> GetBacktests(int id = 0)
        {
            var backtests = _context.Backtest.Where(b => b.UserId == 1).ToList();

            if (id != 0)
                backtests = backtests.Where(b => b.Code == id).ToList();

            foreach (var backtest in backtests)
            {
                var filters = _context.BacktestFilters.Where(bf => bf.BacktestCode == backtest.Code).ToList();
                foreach (var filter in filters)
                    filter.Backtest = null;

                var leagues = _context.BacktestLeaguesList.Where(bl => bl.BacktestCode == backtest.Code).ToList();
                foreach (var league in leagues)
                    league.Backtest = null;

                var leagueSeasons = _context.BacktestLeagueSeasonsList.Where(bls => bls.BacktestCode == backtest.Code).ToList();
                foreach (var leagueSeason in leagueSeasons)
                    leagueSeason.Backtest = null;

                var teams = _context.BacktestTeamsList.Where(bt => bt.BacktestCode == backtest.Code).ToList();
                foreach (var team in teams)
                    team.Backtest = null;

                backtest.Filters = filters;
                backtest.Leagues = leagues;
                backtest.LeagueSeasons = leagueSeasons;
                backtest.Teams = teams;
            }

            return backtests;
        }
    }
}

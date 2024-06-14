using BetPlacer.Backtest.API.Config;
using BetPlacer.Backtest.API.Models;
using BetPlacer.Backtest.API.Models.Entities;
using BetPlacer.Backtest.API.Models.Entities.Filters;
using BetPlacer.Backtest.API.Models.Request;
using BetPlacer.Backtest.API.Models.ValueObjects;
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

        public async Task CreateBacktest(BacktestVO backtestVO)
        {
            var backtest = new BacktestModel(backtestVO);
            _context.Backtest.Add(backtest);

            await _context.SaveChangesAsync();

            if (backtestVO.Filters != null && backtestVO.Filters.Count > 0)
            {
                foreach (var filter in backtestVO.Filters)
                {
                    var newFilter = new BacktestFilterModel(backtest.Code, filter);
                    _context.BacktestFilters.Add(newFilter);
                }

                await _context.SaveChangesAsync();
            }

            if (backtestVO.AdditionalInformation != null && backtestVO.AdditionalInformation.Count > 0)
            { 
                foreach (var info in backtestVO.AdditionalInformation)
                {
                    var newInfo = new BacktestAdditionalInformationModel(backtest.Code, info.Info);
                    _context.BacktestAdditionalInformation.Add(newInfo);
                }

                await _context.SaveChangesAsync();
            }
        }


        public void UpdateFilters(int backtestCode)
        {
            _context.Database.ExecuteSql($"UPDATE backtest SET uses_in_fixture = TRUE WHERE code = {backtestCode}");

            var backtest = _context.Backtest.SingleOrDefault(b => b.Code == backtestCode);
            if (backtest != null)
            {
                _context.Entry(backtest).Reload();
            }
        }

        public List<BacktestVO> GetBacktests(bool onlyWithFilterFixture, int id = 0)
        {
            List<BacktestVO> backtestsToReturn = new List<BacktestVO>();

            var backtests = _context.Backtest.Where(b => b.UserId == 1).ToList();

            if (id != 0)
                backtests = backtests.Where(b => b.Code == id).ToList();

            foreach (var backtest in backtests)
            {
                var filters = _context.BacktestFilters.Where(bf => bf.BacktestCode == backtest.Code).ToList();
                var additionalInfos = _context.BacktestAdditionalInformation.Where(bai => bai.BacktestCode == backtest.Code).ToList();
                var backtestVO = new BacktestVO(backtest, filters, additionalInfos);

                if (!onlyWithFilterFixture || backtest.UsesInFixture)
                    backtestsToReturn.Add(backtestVO);
            }

            return backtestsToReturn;
        }

        public async Task SaveFixtureFilters(List<BacktestFilterFixtureRequestModel> fixtureFilters)
        {
            foreach (var fixtureFilter in fixtureFilters)
            {
                var existentFilter = _context.BacktestFixtureFilter.FirstOrDefault(bff => bff.BacktestCode == fixtureFilter.BacktestCode && bff.FixtureCode == fixtureFilter.FixtureCode);

                if (existentFilter == null)
                {
                    BacktestFixtureFilterModel fixtureFilterDb = new BacktestFixtureFilterModel();
                    fixtureFilterDb.FixtureCode = fixtureFilter.FixtureCode;
                    fixtureFilterDb.BacktestCode = fixtureFilter.BacktestCode;
                    _context.BacktestFixtureFilter.Add(fixtureFilterDb);
                }
            }

            await _context.SaveChangesAsync();
        }

        public List<BacktestFixture> GetFixtureBacktests(List<int> fixtureCodes)
        {
            List<BacktestFixture> backtestFixtures = new List<BacktestFixture>();

            var fixtureFilters = _context.BacktestFixtureFilter.Where(f => fixtureCodes.Contains(f.FixtureCode)).ToList();

            if (fixtureFilters != null && fixtureFilters.Count > 0)
            {
                var backtestCodes = fixtureFilters.Select(ff => ff.BacktestCode).ToList();
                var backtests = _context.Backtest.Where(b => backtestCodes.Contains(b.Code)).ToList();

                foreach (var fixtureFilter in fixtureFilters)
                {
                    var backtest = backtests.FirstOrDefault(b => b.Code == fixtureFilter.BacktestCode);
                    BacktestFixture backtestFixture = new BacktestFixture(backtest, fixtureFilter.FixtureCode);
                    backtestFixtures.Add(backtestFixture);
                }
            }

            return backtestFixtures;
        }

        public List<FilterBacktestModel> GetFilters()
        {
            var filters = _context.Filter.ToList();
            return filters;
        }
    }
}

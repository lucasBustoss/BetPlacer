using BetPlacer.Punter.API.Config;
using BetPlacer.Punter.API.Models;
using BetPlacer.Punter.API.Models.Entities;
using BetPlacer.Punter.API.Models.ValueObjects.Intervals;
using BetPlacer.Punter.API.Models.ValueObjects.Strategy;
using BetPlacer.Punter.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

public class PunterRepository : IPunterRepository
{
    private readonly PunterDbContext _context;

    public PunterRepository(DbContextOptions<PunterDbContext> db)
    {
        _context = new PunterDbContext(db);
    }

    public List<StrategyInfo> GetBacktestsByLeague(int leagueCode)
    {
        List<StrategyInfo> strategyInfos = new List<StrategyInfo>();
        var backtests = _context.PunterBacktest.Where(pb => pb.LeagueCode == leagueCode && pb.Active == true).ToList();

        if (backtests != null && backtests.Count > 0)
        {
            foreach (var backtest in backtests)
            {

                List<BestInterval> bestIntervals = new List<BestInterval>();
                List<ResultInterval> resultIntervals = new List<ResultInterval>();

                var classifications = _context.PunterBacktestClassification.Where(pbc => pbc.PunterBacktestCode == backtest.Code).ToList();
                var intervals = _context.PunterBacktestInterval.Where(pbi => pbi.PunterBacktestCode == backtest.Code).ToList();
                var combinedIntervals = _context.PunterBacktestCombinedInterval.Where(pbci => pbci.PunterBacktestCode == backtest.Code).ToList();

                foreach (var interval in intervals)
                {
                    BestInterval bestInterval = new BestInterval(interval.Name, interval.InitialValue, interval.FinalValue, interval.CoefficientVariation, interval.InferiorLimit);
                    bestIntervals.Add(bestInterval);
                }

                foreach (var combinedInterval in combinedIntervals)
                {
                    ResultInterval resultInterval = new ResultInterval(
                        combinedInterval.Name, combinedInterval.PercentMatches, combinedInterval.Result, combinedInterval.CoefficientVariation, combinedInterval.InferiorLimit, combinedInterval.Active, combinedInterval.Code);

                    resultIntervals.Add(resultInterval);
                }

                StrategyInfo strategyInfo = new StrategyInfo(backtest.Code, backtest.Name, classifications.Select(s => s.Classification).ToList(), backtest.ResultAfterClassification, bestIntervals, resultIntervals);
                strategyInfos.Add(strategyInfo);
            }
        }

        return strategyInfos;
    }

    public List<FixtureStrategyModel> GetFixturesStrategy()
    {
        var fixturesStrategy = _context.FixtureStrategy.ToList();
        return fixturesStrategy;
    }

    public PunterBacktestModel GetBacktestByLeagueCodeAndStrategyName(int leagueCode, string strategyName)
    {
        var backtest = _context.PunterBacktest.Where(pb => pb.LeagueCode == leagueCode && pb.Name == strategyName).FirstOrDefault();
        return backtest;
    }

    public void ActiveFilter(int strategyCode, int filterCode)
    {
        var backtest = _context.PunterBacktest.Where(pb => pb.Code == strategyCode).FirstOrDefault();

        if (backtest != null)
        {
            var combinedIntervals = _context.PunterBacktestCombinedInterval.Where(pbci => pbci.PunterBacktestCode == backtest.Code).ToList();

            foreach (var combinedInterval in combinedIntervals)
                combinedInterval.Active = combinedInterval.Code == filterCode;

            _context.SaveChanges();
        }
    }

    public void SaveMatchAnalysis(List<FixtureStrategyModel> fixtureStrategies)
    {
        foreach (var fixtureStrategy in fixtureStrategies)
        {
            var existentFixtureStrategy =
                _context.FixtureStrategy.Where(f => f.FixtureCode == fixtureStrategy.FixtureCode && (f.StrategyName == fixtureStrategy.StrategyName || f.StrategyName == null)).FirstOrDefault();

            if (existentFixtureStrategy == null)
                _context.FixtureStrategy.Add(fixtureStrategy);
        }

        _context.SaveChanges();
    }

    public async Task Create(int leagueCode, List<StrategyInfo> strategies)
    {
        foreach (StrategyInfo strategy in strategies)
        {
            PunterBacktestModel existentBacktest = GetBacktestByLeagueCodeAndStrategyName(leagueCode, strategy.Name);

            if (existentBacktest != null)
                DeleteBacktest(existentBacktest);

            PunterBacktestModel backtest = new PunterBacktestModel(strategy.Name, leagueCode, strategy.ResultAfterClassification);

            _context.PunterBacktest.Add(backtest);
            await _context.SaveChangesAsync();

            if (strategy.Classifications.Count > 0)
            {

                foreach (string strategyClassification in strategy.Classifications)
                {
                    PunterBacktestClassificationModel classification = new PunterBacktestClassificationModel(backtest.Code, strategyClassification);
                    _context.PunterBacktestClassification.Add(classification);
                }

                await _context.SaveChangesAsync();
            }

            if (strategy.BestIntervals.Count > 0)
            {
                foreach (BestInterval strategyInterval in strategy.BestIntervals)
                {
                    PunterBacktestIntervalModel interval = new PunterBacktestIntervalModel(
                        backtest.Code,
                        strategyInterval.PropertyName,
                        strategyInterval.InitialInterval,
                        strategyInterval.FinalInterval,
                        strategyInterval.CoefficientVariation,
                        strategyInterval.InferiorLimit);

                    _context.PunterBacktestInterval.Add(interval);
                }

                await _context.SaveChangesAsync();
            }

            if (strategy.ResultAfterIntervals.Count > 0)
            {
                foreach (ResultInterval strategyResult in strategy.ResultAfterIntervals)
                {
                    PunterBacktestCombinedIntervalModel result = new PunterBacktestCombinedIntervalModel(
                        backtest.Code,
                        strategyResult.Name,
                        strategyResult.PercentMatches,
                        strategyResult.Result,
                        strategyResult.CoefficientVariation,
                        strategyResult.InferiorLimit);

                    _context.PunterBacktestCombinedInterval.Add(result);
                }

                await _context.SaveChangesAsync();
            }
        }
    }

    private void DeleteBacktest(PunterBacktestModel backtest)
    {
        var backtestClassifications = _context.PunterBacktestClassification.Where(pb => pb.PunterBacktestCode == backtest.Code).ToList();
        var backtestIntervals = _context.PunterBacktestInterval.Where(pb => pb.PunterBacktestCode == backtest.Code).ToList();
        var backtestCombinedIntervals = _context.PunterBacktestCombinedInterval.Where(pb => pb.PunterBacktestCode == backtest.Code).ToList();

        foreach (var combinedInterval in backtestCombinedIntervals)
            _context.Remove(combinedInterval);

        foreach (var interval in backtestIntervals)
            _context.Remove(interval);

        foreach (var classification in backtestClassifications)
            _context.Remove(classification);

        _context.Remove(backtest);
    }

    public async Task<List<MatchBaseData>> GetMatchBaseDataAsync(int leagueCode)
    {
        var query = Queries.GetMatchBaseDataQuery(leagueCode);

        List<MatchBaseData> baseData = await _context.MatchBaseData
            .FromSqlInterpolated(FormattableStringFactory.Create(query))
            .ToListAsync();

        return baseData;
    }

    public async Task<List<MatchBaseData>> GetLastMatches(int leagueCode)
    {
        var query = Queries.GetLastMatches(leagueCode);

        List<MatchBaseData> baseData = await _context.MatchBaseData
            .FromSqlInterpolated(FormattableStringFactory.Create(query))
            .ToListAsync();

        return baseData;
    }

    public async Task<List<NextMatch>> GetNextMatches(string date, int leagueCode)
    {
        var query = Queries.GetNextMatches(date, leagueCode);

        List<NextMatch> baseData = await _context.NextMatch
            .FromSqlInterpolated(FormattableStringFactory.Create(query))
            .ToListAsync();

        return baseData;
    }
}
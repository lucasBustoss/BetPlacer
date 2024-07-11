using BetPlacer.Fixtures.Config;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Fixtures.API.Models.Enums;
using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.ValueObjects;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Fixtures.API.Services;
using System.Diagnostics;
using System.Collections.Concurrent;
using BetPlacer.Fixtures.API.Messages.ModelToMessage;
using BetPlacer.Backtest.API.Models;
using BetPlacer.Core.API.Models.Request.PinnacleOdds;
using BetPlacer.Fixtures.API.Utils;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.LeagueFixtureByDate;

namespace BetPlacer.Fixtures.API.Repositories
{
    public class FixturesRepository : IFixturesRepository
    {
        private readonly FixturesDbContext _context;
        private readonly CalculateFixtureStatsService _calculateService;

        public FixturesRepository(DbContextOptions<FixturesDbContext> db)
        {
            _context = new FixturesDbContext(db);
            _calculateService = new CalculateFixtureStatsService();
            //_messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        }

        public IEnumerable<Fixture> List(FixtureListSearchType searchType, IEnumerable<LeaguesApiResponseModel> leagues, IEnumerable<TeamsApiResponseModel> teams, bool withGoals, bool withStats, bool saveAsMessage, string backtestHash)
        {
            var fixturesVO = new ConcurrentBag<Fixture>();

            List<FixtureStatsTradeModel> stats = new List<FixtureStatsTradeModel>();
            List<FixtureGoalsModel> goals = new List<FixtureGoalsModel>();

            var query = _context.Fixtures.AsQueryable();

            switch (searchType)
            {
                case FixtureListSearchType.All:
                    break;

                case FixtureListSearchType.OnlyCompleted:
                    query = query.Where(a => a.Status == "complete");
                    break;

                case FixtureListSearchType.OnlyNext:
                    query = query.Where(f => f.Status == "incomplete");
                    break;

                default:
                    throw new ArgumentException("Tipo de consulta não suportado", nameof(searchType));
            }

            var fixtures = query.ToList();
            var fixtureCodes = fixtures.Select(f => f.Code).ToList();

            if (withGoals)
                goals = _context.FixtureGoals.Where(fg => fixtureCodes.Contains(fg.FixtureCode)).ToList();

            if (withStats)
                stats = _context.FixtureStatsTrade.Where(fst => fixtureCodes.Contains(fst.FixtureCode)).ToList();

            var leaguesBySeasonCode = leagues.SelectMany(l => l.Season.Select(s => new { League = l, s.Code }))
                                             .ToDictionary(x => x.Code, x => x.League);

            var teamsByCode = teams.ToDictionary(t => t.Code, t => t);

            var goalsByFixtureCode = goals.GroupBy(g => g.FixtureCode)
                                          .ToDictionary(g => g.Key, g => g.ToList());

            var statsByFixtureCode = stats.ToDictionary(s => s.FixtureCode, s => s);


            if (saveAsMessage)
                _ = TreatFixtureObjects(fixtures, leaguesBySeasonCode, teamsByCode, goalsByFixtureCode, statsByFixtureCode, true, backtestHash);
            else
                fixturesVO = TreatFixtureObjects(fixtures, leaguesBySeasonCode, teamsByCode, goalsByFixtureCode, statsByFixtureCode, false, backtestHash);

            return fixturesVO.ToList();
        }

        public IEnumerable<FixtureModel> GetFixturesWithoutOdds(LeaguesApiResponseModel league)
        {
            var leagueSeasonCodes = league.Season.Select(s => s.Code).ToList();
            var fixturesWithoutOdds = _context.Fixtures
                                      .Where(f => !_context.FixtureOdds
                                                        .Any(fo => fo.FixtureCode == f.Code)
                                                  && leagueSeasonCodes.Contains(f.SeasonCode)
                                                  && f.StartDate < DateTime.UtcNow.AddDays(-1))
                                      .ToList();

            var query = from fixture in _context.Fixtures
                                                         join odd in _context.FixtureOdds on fixture.Code equals odd.FixtureCode
                                                         where leagueSeasonCodes.Contains(fixture.SeasonCode) &&
                                                               (odd.HomeOdd == 0 || odd.DrawOdd == 0 || odd.AwayOdd == 0 ||
                                                                odd.Over25Odd == 0 || odd.Under25Odd == 0 ||
                                                                odd.BTTSYesOdd == 0 || odd.BTTSNoOdd == 0)
                                                         select fixture;

            var fixtures = query.ToList();

            var combinedFixtures = new List<FixtureModel>();
            var addedFixtureCodes = new HashSet<int>();

            foreach (var fixture in fixturesWithoutOdds)
            {
                if (addedFixtureCodes.Add(fixture.Code))
                    combinedFixtures.Add(fixture);
            }

            foreach (var fixture in fixtures)
            {
                if (addedFixtureCodes.Add(fixture.Code))
                    combinedFixtures.Add(fixture);
            }

            return combinedFixtures.OrderBy(f => f.StartDate).ToList();
        }

        public List<int> GetFixtureCodesByDate(DateTime startDate, DateTime finalDate)
        {
            var fixtures = _context.Fixtures.Where(f => f.StartDate >= startDate.AddHours(3) && f.StartDate <= finalDate.AddHours(3)).ToList();

            return fixtures.Select(f => f.Code).ToList();
        }

        public List<LeagueFixtureByDate> ListFixturesByDate(IEnumerable<LeaguesApiResponseModel> leagues, IEnumerable<TeamsApiResponseModel> teams, IEnumerable<PunterBacktestFixture> fixturesStrategy)
        {

            List<LeagueFixtureByDate> fixturesByDate = new List<LeagueFixtureByDate>();
            List<FixtureStatsTradeModel> stats = new List<FixtureStatsTradeModel>();

            DateTime endDate = DateTime.UtcNow.AddDays(3).Date.AddMilliseconds(-1);
            var fixtures = _context.Fixtures.Where(f => f.StartDate >= DateTime.UtcNow.Date && f.StartDate <= endDate).ToList();
            var fixtureCodes = fixtures.Select(f => f.Code).ToList();

            stats = _context.FixtureStatsTrade.Where(fst => fixtureCodes.Contains(fst.FixtureCode)).ToList();

            for (var i = 0; i < 3; i++)
            {
                DateTime date = DateTime.Now.AddDays(i);

                LeagueFixtureByDate fixtureByDate = new LeagueFixtureByDate();
                fixtureByDate.Date = date.ToString("dd/MM/yyyy");

                var fixturesCurrentDate = fixtures.Where(f => f.StartDate.AddHours(-3).ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd")).OrderBy(f => f.SeasonCode).ToList();

                foreach (var fixtureCurrentDate in fixturesCurrentDate)
                {
                    var league = leagues.FirstOrDefault(league => league.Season.Any(season => season.Code == fixtureCurrentDate.SeasonCode));
                    var fixtureStat = stats.FirstOrDefault(s => s.FixtureCode == fixtureCurrentDate.Code);

                    if (league == null)
                        continue;

                    LeagueFixtures leagueFixtures = fixtureByDate.LeagueFixtures.FirstOrDefault(lf => lf.LeagueCode == league.Code);

                    if (leagueFixtures == null)
                    {
                        leagueFixtures = new LeagueFixtures(league);
                        fixtureByDate.LeagueFixtures.Add(leagueFixtures);
                    }

                    string filters = "-";
                    bool analyzedFixture = false;
                    if (fixturesStrategy != null && fixturesStrategy.Count() > 0)
                    {
                        bool isFirst = true;

                        foreach (var fixtureStrategy in fixturesStrategy)
                        {
                            var existentFixtureInStrategy = fixtureStrategy.FixtureCode == fixtureCurrentDate.Code;

                            if (!existentFixtureInStrategy)
                                continue;

                            string strategy = fixtureStrategy.StrategyName;
                            analyzedFixture = true;

                            if (isFirst)
                            {
                                filters = strategy;
                                isFirst = false;
                            }
                            else
                                filters += $" - {strategy}";
                        }
                    }

                    FixtureOdds odd = _context.FixtureOdds.Where(f => f.FixtureCode == fixtureCurrentDate.Code).FirstOrDefault();
                    leagueFixtures.Fixtures.Add(new FixtureDate(fixtureCurrentDate, fixtureStat, filters, odd, analyzedFixture));
                }

                foreach (var leagueFixture in fixtureByDate.LeagueFixtures)
                    leagueFixture.Fixtures = leagueFixture.Fixtures.OrderBy(f => f.Date).ToList();

                fixtureByDate.LeagueFixtures = fixtureByDate.LeagueFixtures.OrderBy(lf => lf.LeagueCountry).ThenBy(lf => lf.LeagueName).ToList();

                fixturesByDate.Add(fixtureByDate);
            }

            return fixturesByDate.OrderBy(fbd => fbd.Date).ToList();
        }

        public void CreateOrUpdateCompleteFixtures(IEnumerable<FixturesFootballResponseModel> fixturesResponse)
        {
            #region Fixtures
            var existingFixtures = _context.Fixtures.ToDictionary(fixture => fixture.Code);

            List<FixtureModel> fixturesSaved = new List<FixtureModel>();
            Stopwatch st = new Stopwatch();

            st.Start();
            foreach (var fixtureResponse in fixturesResponse)
            {
                var fixtureModel = new FixtureModel(fixtureResponse);

                if (!existingFixtures.ContainsKey(fixtureResponse.Code))
                    _context.Fixtures.Add(fixtureModel);
                else
                {
                    var oldFixture = existingFixtures[fixtureResponse.Code];

                    if (oldFixture.Status == "incomplete")
                    {
                        var newFixture = fixtureModel;
                        newFixture.Code = oldFixture.Code;
                        _context.Entry(oldFixture).CurrentValues.SetValues(newFixture);
                    }
                }

                fixturesSaved.Add(fixtureModel);
            }

            _context.SaveChanges();
            st.Stop();
            double elapsedSeconds = st.Elapsed.TotalSeconds;

            Console.WriteLine($"Tempo pra gravar jogos: {elapsedSeconds} segundos");
            #endregion

            #region FixtureGoals

            CreateFixtureGoals(fixturesResponse, fixturesSaved);

            #endregion
        }

        public List<string> CreateOrUpdateNextFixtures(IEnumerable<FixturesFootballResponseModel> fixturesResponse, List<PinnacleOddsModel> odds)
        {
            List<string> teamsNotFound = new List<string>();

            #region Fixtures

            var existingFixtures = _context.Fixtures.ToDictionary(fixture => fixture.Code);
            List<FixtureModel> fixturesSaved = new List<FixtureModel>();

            foreach (var fixtureResponse in fixturesResponse)
            {
                var fixtureModel = new FixtureModel(fixtureResponse);

                if (!existingFixtures.ContainsKey(fixtureResponse.Code))
                {
                    _context.Fixtures.Add(fixtureModel);
                    fixturesSaved.Add(fixtureModel);
                }
                else
                {
                    var oldFixture = existingFixtures[fixtureResponse.Code];

                    var newFixture = fixtureModel;
                    newFixture.Code = oldFixture.Code;
                    _context.Entry(oldFixture).CurrentValues.SetValues(newFixture);
                }
            }

            _context.SaveChanges();

            #endregion

            #region FixtureOdds

            if (odds != null)
            {
                foreach (PinnacleOddsModel odd in odds)
                {
                    string homeFootyStatsName = FixtureUtils.GetFootyStatsNameByPinnacleName(odd.HomeTeam);
                    string awayFootyStatsName = FixtureUtils.GetFootyStatsNameByPinnacleName(odd.AwayTeam);

                    FixturesFootballResponseModel fixtureToOdd = fixturesResponse.Where(fr =>
                            (fr.HomeTeamName == homeFootyStatsName ||
                            fr.AwayTeamName == awayFootyStatsName) &&
                            FixtureUtils.TimestampToDatetime(fr.DateTimestamp).AddHours(3).ToString("yyyy-MM-dd'T'HH:mm:ss") == odd.Date)
                        .FirstOrDefault();

                    if (fixtureToOdd != null)
                    {
                        CreateOrUpdateOdds(new FixtureOdds(fixtureToOdd.Code, odd.HomeOdd, odd.DrawOdd, odd.AwayOdd, odd.Over25Odd, odd.Under25Odd, odd.BttsYesOdd.Value, odd.BttsNoOdd.Value));
                    }
                    // Só vou enviar a mensagem pro Telegram caso o nome do time da Pinnacle seja igual antes e depois de converter.
                    // Isso pode ser um indicativo de que não tenho o parse correto
                    else if (homeFootyStatsName == odd.HomeTeam || awayFootyStatsName == odd.AwayTeam)
                        teamsNotFound.Add($"{homeFootyStatsName} x {awayFootyStatsName}");
                }
            }

            return teamsNotFound;

            #endregion
        }

        public void CreateOrUpdateOdds(FixtureOdds odds)
        {
            var existingOdd = _context.FixtureOdds.Where(o => o.FixtureCode == odds.FixtureCode).FirstOrDefault();

            if (existingOdd != null)
            {
                var newOdds = odds;
                newOdds.Code = existingOdd.Code;
                _context.Entry(existingOdd).CurrentValues.SetValues(newOdds);
                _context.SaveChanges();
            }
            else
            {
                _context.FixtureOdds.Add(odds);
                _context.SaveChanges();
            }
        }

        public void CalculateFixtureStats(int leagueSeasonCode)
        {
            List<FixtureModel> fixturesToCalculate = new List<FixtureModel>();

            IEnumerable<FixtureModel> fixtures =
                _context.Fixtures
                .Where(f => f.SeasonCode == leagueSeasonCode)
                .OrderBy(f => f.StartDate)
                .ToList();

            foreach (var fixture in fixtures)
            {
                if (fixture.Status == "complete")
                {
                    fixturesToCalculate.Add(fixture);
                    continue;
                }

                FixtureModel existentFixtureInList = fixturesToCalculate
                .Where(f => f.Status == "incomplete" &&
                ((f.HomeTeamId == fixture.HomeTeamId || f.HomeTeamId == fixture.AwayTeamId) ||
                (f.AwayTeamId == fixture.HomeTeamId || f.AwayTeamId == fixture.AwayTeamId)))
                .FirstOrDefault();

                if (fixture.Status == "incomplete" && existentFixtureInList == null && fixture.StartDate > DateTime.UtcNow)
                {
                    fixturesToCalculate.Add(fixture);
                    continue;
                }
            }

            var fixtureCodes = fixturesToCalculate.Select(f => f.Code).ToList();

            IEnumerable<FixtureGoalsModel> fixturesGoals =
                _context.FixtureGoals
                .Where(fg => fixtureCodes.Contains(fg.FixtureCode))
                .ToList();

            var stats = _calculateService.Calculate(fixturesToCalculate, fixturesGoals);
            var existentStats = _context.FixtureStatsTrade.Where(fst => fixtureCodes.Contains(fst.FixtureCode));

            foreach (var stat in stats)
            {
                var existentStat = existentStats.FirstOrDefault(es => es.FixtureCode == stat.FixtureCode);

                if (existentStat == null)
                    _context.FixtureStatsTrade.Add(stat);
            }

            _context.SaveChanges();
        }

        #region Private methods

        private void CreateFixtureGoals(IEnumerable<FixturesFootballResponseModel> fixturesResponse, List<FixtureModel> fixturesSaved)
        {
            Stopwatch st = new Stopwatch();
            st.Start();

            #region HomeGoals

            var fixtureHomeGoals = fixturesSaved.SelectMany(fixtureBd =>
            {
                var fixtureResponse = fixturesResponse.FirstOrDefault(fixture => fixture.Code == fixtureBd.Code);

                if (fixtureResponse != null)
                    return fixtureResponse.HomeGoalsMinutes.Select(goalMinute => new FixtureGoalsModel(fixtureBd.Code, goalMinute.Replace('+', '.'), fixtureBd.HomeTeamId));

                return Enumerable.Empty<FixtureGoalsModel>();
            }).ToList();

            #endregion

            #region AwayGoals

            var fixtureAwayGoals = fixturesSaved.SelectMany(fixtureBd =>
            {
                var fixtureResponse = fixturesResponse.FirstOrDefault(fixture => fixture.Code == fixtureBd.Code);

                if (fixtureResponse != null)
                    return fixtureResponse.AwayGoalsMinutes.Select(goalMinute => new FixtureGoalsModel(fixtureBd.Code, goalMinute.Replace('+', '.'), fixtureBd.AwayTeamId));

                return Enumerable.Empty<FixtureGoalsModel>();
            }).ToList();

            #endregion

            var goals = new List<FixtureGoalsModel>();
            goals.AddRange(fixtureHomeGoals);
            goals.AddRange(fixtureAwayGoals);

            foreach (var goal in goals)
            {
                var fixtureGoalBd = _context.FixtureGoals.FirstOrDefault(
                    fixtureGoal => fixtureGoal.FixtureCode == goal.FixtureCode && fixtureGoal.Minute == goal.Minute && fixtureGoal.TeamId == goal.TeamId);

                if (fixtureGoalBd == null)
                    _context.FixtureGoals.Add(goal);
            }

            _context.SaveChanges();

            st.Stop();
            double elapsedSeconds = st.Elapsed.TotalSeconds;

            Console.WriteLine($"Tempo pra gravar gols: {elapsedSeconds} segundos");
        }

        private ConcurrentBag<Fixture> TreatFixtureObjects(List<FixtureModel> fixtures, Dictionary<int, LeaguesApiResponseModel> leaguesBySeasonCode, Dictionary<int, TeamsApiResponseModel> teamsByCode, Dictionary<int, List<FixtureGoalsModel>> goalsByFixtureCode, Dictionary<int, FixtureStatsTradeModel> statsByFixtureCode, bool saveAsMessage, string backtestHash)
        {
            var fixturesVO = new ConcurrentBag<Fixture>();

            if (saveAsMessage)
            {
                var fixturesSorted = fixtures.OrderBy(f => f.StartDate).ToList();
                foreach (var fixture in fixturesSorted)
                {
                    leaguesBySeasonCode.TryGetValue(fixture.SeasonCode, out var league);
                    teamsByCode.TryGetValue(fixture.HomeTeamId, out var homeTeam);
                    teamsByCode.TryGetValue(fixture.AwayTeamId, out var awayTeam);
                    goalsByFixtureCode.TryGetValue(fixture.Code, out var goalsFixture);
                    statsByFixtureCode.TryGetValue(fixture.Code, out var statFixture);

                    if (goalsFixture != null)
                    {
                        foreach (var goal in goalsFixture)
                            goal.Fixture = null;
                    }

                    if (statFixture != null)
                        statFixture.Fixture = null;

                    if (league != null && homeTeam != null && awayTeam != null)
                    {
                        var fixtureVO = new Fixture(fixture, league, homeTeam, awayTeam, goalsFixture, statFixture);
                        var message = new FixtureMessage { Fixture = fixtureVO };
                        //_messageSender.SendMessage<FixtureMessage>(message, $"backtest_{backtestHash}");
                    }
                }

                // Send end of messages signal
                //_messageSender.SendEndOfMessagesSignal($"backtest_{backtestHash}");
            }
            else
            {
                foreach (var fixture in fixtures)
                {
                    leaguesBySeasonCode.TryGetValue(fixture.SeasonCode, out var league);
                    teamsByCode.TryGetValue(fixture.HomeTeamId, out var homeTeam);
                    teamsByCode.TryGetValue(fixture.AwayTeamId, out var awayTeam);
                    goalsByFixtureCode.TryGetValue(fixture.Code, out var goalsFixture);
                    statsByFixtureCode.TryGetValue(fixture.Code, out var statFixture);

                    if (goalsFixture != null)
                    {
                        foreach (var goal in goalsFixture)
                            goal.Fixture = null;
                    }

                    if (statFixture != null)
                        statFixture.Fixture = null;

                    if (league != null && homeTeam != null && awayTeam != null)
                    {
                        var fixtureVO = new Fixture(fixture, league, homeTeam, awayTeam, goalsFixture, statFixture);
                        fixturesVO.Add(fixtureVO);
                    }
                }
            }

            return fixturesVO;
        }

        #endregion
    }
}

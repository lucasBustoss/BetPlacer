using BetPlacer.Fixtures.Config;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Fixtures.API.Models.Enums;
using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.ValueObjects;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Fixtures.API.Services;

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
        }

        public IEnumerable<Fixture> List(FixtureListSearchType searchType, IEnumerable<LeaguesApiResponseModel> leagues, IEnumerable<TeamsApiResponseModel> teams)
        {
            List<Fixture> fixturesVO = new List<Fixture>();

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

            foreach (FixtureModel fixture in fixtures)
            {
                LeaguesApiResponseModel league = leagues.FirstOrDefault(l => l.Season.Any(s => s.Id == fixture.SeasonCode));
                TeamsApiResponseModel homeTeam = teams.FirstOrDefault(t => t.Code == fixture.HomeTeamId);
                TeamsApiResponseModel awayTeam = teams.FirstOrDefault(t => t.Code == fixture.AwayTeamId);

                if (league != null && homeTeam != null && awayTeam != null)
                {
                    Fixture fixtureVO = new Fixture(fixture, league, homeTeam, awayTeam);
                    fixturesVO.Add(fixtureVO);
                }
            }

            return fixturesVO;
        }

        public async Task CreateOrUpdateCompleteFixtures(IEnumerable<FixturesFootballResponseModel> fixturesResponse)
        {
            #region Fixtures

            var existingFixtures = _context.Fixtures.ToDictionary(fixture => fixture.Code);

            List<FixtureModel> fixturesSaved = new List<FixtureModel>();

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

            await _context.SaveChangesAsync();

            #endregion

            #region FixtureGoals

            await CreateFixtureGoals(fixturesResponse, fixturesSaved);

            #endregion
        }

        public async Task CreateNextFixtures(IEnumerable<FixturesFootballResponseModel> fixturesResponse)
        {
            #region Fixtures

            var existingFixtures = _context.Fixtures.ToDictionary(fixture => fixture.Code);
            List<FixtureModel> fixturesSaved = new List<FixtureModel>();

            foreach (var fixtureResponse in fixturesResponse)
            {
                if (!existingFixtures.ContainsKey(fixtureResponse.Code))
                {
                    var fixtureModel = new FixtureModel(fixtureResponse);
                    _context.Fixtures.Add(fixtureModel);

                    fixturesSaved.Add(fixtureModel);
                }
            }

            await _context.SaveChangesAsync();

            #endregion

            #region FixtureGoals

            await CreateFixtureGoals(fixturesResponse, fixturesSaved);

            #endregion
        }

        public async Task CalculateFixtureStats(int leagueSeasonCode)
        {
            List<FixtureModel> fixturesToCalculate = new List<FixtureModel>();

            IEnumerable<FixtureModel> fixtures =
                await _context.Fixtures
                .Where(f => f.SeasonCode == leagueSeasonCode)
                .OrderBy(f => f.StartDate)
                .ToListAsync();

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

            var teste = fixturesToCalculate.Where(f => f.Status == "incomplete").Select(f => new { f.Code, f.StartDate, f.HomeTeamName, f.AwayTeamName }).ToList();
            var fixtureCodes = fixturesToCalculate.Select(f => f.Code).ToList();

            IEnumerable<FixtureGoalsModel> fixturesGoals =
                await _context.FixtureGoals
                .Where(fg => fixtureCodes.Contains(fg.FixtureCode))
                .ToListAsync();

            var stats = _calculateService.Calculate(fixturesToCalculate, fixturesGoals);
        }

        #region Private methods

        private async Task CreateFixtureGoals(IEnumerable<FixturesFootballResponseModel> fixturesResponse, List<FixtureModel> fixturesSaved)
        {
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
                {
                    _context.FixtureGoals.Add(goal);
                    await _context.SaveChangesAsync();
                }
            }
        }

        #endregion
    }
}

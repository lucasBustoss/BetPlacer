using BetPlacer.Fixtures.Config;
using BetPlacer.Fixtures.API.Models;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Core.Models.Response.API.Fixtures;

namespace BetPlacer.Fixtures.API.Repositories
{
    public class FixturesRepository : IFixturesRepository
    {
        private readonly FixturesDbContext _context;

        public FixturesRepository(DbContextOptions<FixturesDbContext> db)
        {
            _context = new FixturesDbContext(db);
        }

        //public IEnumerable<Fixture> List()
        //{
        //    List<Fixture> fixturesVO = new List<Fixture>();
        //    var fixtures = _context.Fixtures.ToList();

        //    fixturesVO.AddRange(fixtures.Select(fixture => new Fixture(fixture)));

        //    return fixturesVO;
        //}

        public async Task CreateOrUpdateCompleteFixtures(IEnumerable<FixturesResponseModel> fixturesResponse)
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

        public async Task CreateNextFixtures(IEnumerable<FixturesResponseModel> fixturesResponse)
        {
            #region Fixtures

            var existingFixtures = _context.Fixtures.ToDictionary(team => team.Code);
            List<FixtureModel> fixturesSaved = new List<FixtureModel>();

            foreach (var fixtureResponse in fixturesResponse)
            {
                if (!existingFixtures.ContainsKey(fixtureResponse.Code))
                {
                    var teamModel = new FixtureModel(fixtureResponse);
                    _context.Fixtures.Add(teamModel);

                    fixturesSaved.Add(teamModel);
                }
            }

            await _context.SaveChangesAsync();

            #endregion

            #region FixtureGoals

            await CreateFixtureGoals(fixturesResponse, fixturesSaved);

            #endregion
        }

        #region Private methods

        private async Task CreateFixtureGoals(IEnumerable<FixturesResponseModel> fixturesResponse, List<FixtureModel> fixturesSaved)
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

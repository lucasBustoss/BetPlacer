using BetPlacer.Core.API.Models.Request.PinnacleOdds;
using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Fixtures.API.Models;

namespace BetPlacer.Fixtures.API.Repositories
{
    public interface IFixturesRepository
    {
        /*IEnumerable<Fixture> List();*/
        Task CreateOrUpdateCompleteFixtures(IEnumerable<FixturesFootballResponseModel> fixtures);
        Task<List<string>> CreateOrUpdateNextFixtures(IEnumerable<FixturesFootballResponseModel> fixtures, List<PinnacleOddsModel> odds);
    }
}

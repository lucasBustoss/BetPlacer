using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Fixtures.API.Models;

namespace BetPlacer.Fixtures.API.Repositories
{
    public interface IFixturesRepository
    {
        /*IEnumerable<Fixture> List();*/
        Task CreateOrUpdateCompleteFixtures(IEnumerable<FixturesFootballResponseModel> fixtures);
        Task CreateNextFixtures(IEnumerable<FixturesFootballResponseModel> fixtures);
    }
}

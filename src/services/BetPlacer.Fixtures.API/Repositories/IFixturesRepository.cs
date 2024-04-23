using BetPlacer.Core.Models.Response.API.Fixtures;
using BetPlacer.Fixtures.API.Models;

namespace BetPlacer.Fixtures.API.Repositories
{
    public interface IFixturesRepository
    {
        /*IEnumerable<Fixture> List();*/
        Task CreateOrUpdateCompleteFixtures(IEnumerable<FixturesResponseModel> fixtures);
        Task CreateNextFixtures(IEnumerable<FixturesResponseModel> fixtures);
    }
}

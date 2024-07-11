using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using BetPlacer.Core.Models.Response.FootballAPI.Teams;

namespace BetPlacer.Core.API.Service.NewFootballApi
{
    public interface INewFootballApiService
    {
        Task<IEnumerable<LeaguesFootballResponseModel>> GetLeagues();
        Task<IEnumerable<TeamsFootballResponseModel>> GetTeams();
        Task<IEnumerable<FixturesFootballResponseModel>> GetCompleteFixtures();
        Task<IEnumerable<FixturesFootballResponseModel>> GetNextFixtures();
    }
}

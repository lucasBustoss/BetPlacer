using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using BetPlacer.Core.Models.Response.FootballAPI.Teams;

namespace BetPlacer.Core.API.Service.FootballApi
{
    public interface IFootballApiService
    {
        public Task<IEnumerable<LeaguesFootballResponseModel>> GetLeagues();
        public Task<IEnumerable<TeamsFootballResponseModel>> GetTeams(int leagueSeasonCode);
        public Task<IEnumerable<FixturesFootballResponseModel>> GetCompleteFixtures(int leagueSeasonCode);
        public Task<IEnumerable<FixturesFootballResponseModel>> GetNextFixtures(int leagueSeasonCode);
    }
}

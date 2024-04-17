using BetPlacer.Core.API.Models.Response.Leagues;
using BetPlacer.Core.API.Models.Response.Teams;

namespace BetPlacer.Core.API.Service
{
    public interface IFootballApiService
    {
        public Task<IEnumerable<LeaguesResponseModel>> GetLeagues();
        public Task<IEnumerable<TeamsResponseModel>> GetTeams(int leagueSeasonCode);
    }
}

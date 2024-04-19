using BetPlacer.Core.Models.Response.API.Leagues;
using BetPlacer.Core.Models.Response.API.Teams;

namespace BetPlacer.Core.API.Service
{
    public interface IFootballApiService
    {
        public Task<IEnumerable<LeaguesResponseModel>> GetLeagues();
        public Task<IEnumerable<TeamsResponseModel>> GetTeams(int leagueSeasonCode);
    }
}

using BetPlacer.Core.API.Models.Response.Leagues;

namespace BetPlacer.Core.API.Service
{
    public interface IFootballApiService
    {
        public Task<IEnumerable<LeagueResponseModel>> GetLeagues();
    }
}

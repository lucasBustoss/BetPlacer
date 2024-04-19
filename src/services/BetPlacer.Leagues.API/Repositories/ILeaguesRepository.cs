using BetPlacer.Core.Models.Response.API.Leagues;
using BetPlacer.Leagues.API.Models.ValueObjects;

namespace BetPlacer.Leagues.API.Repositories
{
    public interface ILeaguesRepository
    {
        List<League> List(bool withSeason);
        Task CreateOrUpdate(IEnumerable<LeaguesResponseModel> leagues);
    }
}

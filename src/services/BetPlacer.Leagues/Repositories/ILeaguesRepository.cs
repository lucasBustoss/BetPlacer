using BetPlacer.Core.API.Models.Response.Leagues;
using BetPlacer.Leagues.API.Models;

namespace BetPlacer.Leagues.API.Repositories
{
    public interface ILeaguesRepository
    {
        IEnumerable<LeagueModel> List();
        Task CreateOrUpdate(IEnumerable<LeagueResponseModel> leagues);
    }
}

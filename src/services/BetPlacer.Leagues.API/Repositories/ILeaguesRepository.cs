using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using BetPlacer.Leagues.API.Models.ValueObjects;

namespace BetPlacer.Leagues.API.Repositories
{
    public interface ILeaguesRepository
    {
        IEnumerable<League> List(bool withSeason);
        Task CreateOrUpdate(IEnumerable<LeaguesFootballResponseModel> leagues);
    }
}

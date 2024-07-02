using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using BetPlacer.Leagues.API.Models.ValueObjects;

namespace BetPlacer.Leagues.API.Repositories
{
    public interface ILeaguesRepository
    {
        IEnumerable<League> List(bool withSeason);
        IEnumerable<League> GetLeagueById(int leagueId);
        IEnumerable<League> GetLeaguesWithCurrentSeason();
        void CreateOrUpdate(IEnumerable<LeaguesFootballResponseModel> leagues);
    }
}

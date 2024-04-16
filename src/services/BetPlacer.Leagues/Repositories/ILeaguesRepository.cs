using BetPlacer.Leagues.Models;

namespace BetPlacer.Leagues.API.Repositories
{
    public interface ILeaguesRepository
    {
        IEnumerable<LeagueModel> List();
        void CreateOrUpdate(IEnumerable<LeagueModel> leagues);
    }
}

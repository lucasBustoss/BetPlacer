using BetPlacer.Core.Models.Response.FootballAPI.Teams;
using BetPlacer.Teams.API.Models;
using BetPlacer.Teams.API.Models.ValueObjects;

namespace BetPlacer.Teams.API.Repositories
{
    public interface ITeamsRepository
    {
        IEnumerable<Team> List();
        Task Create(IEnumerable<TeamsFootballResponseModel> teams);
    }
}

using BetPlacer.Core.API.Models.Response.Teams;
using BetPlacer.Teams.API.Models;

namespace BetPlacer.Teams.API.Repositories
{
    public interface ITeamsRepository
    {
        IEnumerable<TeamModel> List();
        Task Create(IEnumerable<TeamsResponseModel> teams);
    }
}

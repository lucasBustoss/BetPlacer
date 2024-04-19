using BetPlacer.Core.Models.Response.API.Teams;
using BetPlacer.Teams.API.Models;

namespace BetPlacer.Teams.API.Repositories
{
    public interface ITeamsRepository
    {
        IEnumerable<TeamModel> List();
        Task Create(IEnumerable<TeamsResponseModel> teams);
    }
}

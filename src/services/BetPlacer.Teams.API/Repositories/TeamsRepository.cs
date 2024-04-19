using BetPlacer.Teams.Config;
using BetPlacer.Teams.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using BetPlacer.Core.Models.Response.API.Teams;

namespace BetPlacer.Teams.API.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly TeamsDbContext _context;

        public TeamsRepository(DbContextOptions<TeamsDbContext> db)
        {
            _context = new TeamsDbContext(db);
        }


        public IEnumerable<TeamModel> List()
        {
            throw new NotImplementedException();
        }

        public async Task Create(IEnumerable<TeamsResponseModel> teamsResponse)
        {
            #region Teams

            var existingTeams = _context.Teams.ToDictionary(team => team.Name);

            foreach (var teamResponse in teamsResponse)
            {
                if (!existingTeams.ContainsKey(teamResponse.Name))
                {
                    var teamModel = new TeamModel(teamResponse);
                    _context.Teams.Add(teamModel);
                }
            }

            await _context.SaveChangesAsync();

            #endregion
        }
    }
}

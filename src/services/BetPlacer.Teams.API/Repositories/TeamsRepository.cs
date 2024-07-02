using BetPlacer.Teams.Config;
using BetPlacer.Teams.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using BetPlacer.Core.Models.Response.FootballAPI.Teams;
using BetPlacer.Teams.API.Models.ValueObjects;

namespace BetPlacer.Teams.API.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly TeamsDbContext _context;

        public TeamsRepository(DbContextOptions<TeamsDbContext> db)
        {
            _context = new TeamsDbContext(db);
        }


        public IEnumerable<Team> List()
        {
            List<Team> teamsVO = new List<Team>();
            var teams = _context.Teams.ToList();

            teamsVO.AddRange(teams.Select(team => new Team(team)));

            return teamsVO;
        }

        public async Task Create(IEnumerable<TeamsFootballResponseModel> teamsResponse)
        {
            #region Teams

            var existingTeams = _context.Teams.ToDictionary(team => team.Name);

            foreach (var teamResponse in teamsResponse)
            {
                if (!existingTeams.ContainsKey(teamResponse.Name.Replace("'", "")))
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

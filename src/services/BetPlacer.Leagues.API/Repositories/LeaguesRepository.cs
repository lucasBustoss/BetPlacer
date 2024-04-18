using BetPlacer.Leagues.Config;
using BetPlacer.Leagues.API.Models;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Core.API.Models.Response.Leagues;
using System.Diagnostics;

namespace BetPlacer.Leagues.API.Repositories
{
    public class LeaguesRepository : ILeaguesRepository
    {
        private readonly LeaguesDbContext _context;

        public LeaguesRepository(DbContextOptions<LeaguesDbContext> db)
        {
            _context = new LeaguesDbContext(db);
        }


        public IEnumerable<LeagueModel> List()
        {
            throw new NotImplementedException();
        }

        public async Task CreateOrUpdate(IEnumerable<LeaguesResponseModel> leaguesResponse)
        {
            #region Leagues

            List<LeagueModel> leaguesSaved = new List<LeagueModel>();

            var existingLeagues = _context.Leagues.ToDictionary(league => league.Name);

            foreach (var leagueResponse in leaguesResponse)
            {
                var leagueModel = new LeagueModel(leagueResponse);

                if (!existingLeagues.ContainsKey(leagueResponse.Name))
                    _context.Leagues.Add(leagueModel);
                else
                    UpdateLeague(existingLeagues[leagueResponse.Name], leagueModel);
                
                leaguesSaved.Add(leagueModel);
            }

            await _context.SaveChangesAsync();

            #endregion

            #region LeagueSeasons

            var leagueSeasons = leaguesSaved.SelectMany(leagueBd =>
            {
                var leagueResponse = leaguesResponse.FirstOrDefault(league => league.Name == leagueBd.Name);

                if (leagueResponse != null)
                    return leagueResponse.Season.Select(season => new LeagueSeasonModel(season, leagueBd.Code.Value));

                return Enumerable.Empty<LeagueSeasonModel>();
            }).ToList();

            await Task.Run(() => CreateSeasons(leagueSeasons));


            #endregion
        }

        #region Private methods

        private void UpdateLeague(LeagueModel oldLeague, LeagueModel newLeague)
        {
            newLeague.Code = oldLeague.Code;
            _context.Entry(oldLeague).CurrentValues.SetValues(newLeague);
        }

        private async Task CreateSeasons(List<LeagueSeasonModel> leagueSeasons)
        {
            foreach (var leagueSeason in leagueSeasons)
            {
                var leagueSeasonBd = _context.LeagueSeasons.FirstOrDefault(league => league.Code == leagueSeason.Code);

                if (leagueSeasonBd == null)
                {
                    _context.LeagueSeasons.Add(leagueSeason);
                    await _context.SaveChangesAsync();
                }
            }
        }

        #endregion
    }
}

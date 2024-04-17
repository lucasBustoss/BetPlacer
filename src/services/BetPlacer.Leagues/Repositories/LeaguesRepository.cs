using BetPlacer.Leagues.Database;
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
            
            Stopwatch stopwatch1 = Stopwatch.StartNew();
            List<LeagueModel> leaguesSaved = new List<LeagueModel>();

            foreach (var leagueResponse in leaguesResponse)
            {
                var leagueModel = new LeagueModel(leagueResponse);
                var leagueBd = _context.Leagues.FirstOrDefault(league => league.Name == leagueResponse.Name);

                if (leagueBd == null)
                    _context.Leagues.Add(leagueModel);
                else
                    UpdateLeague(leagueBd, leagueModel);

                await _context.SaveChangesAsync();
                leaguesSaved.Add(leagueModel);
            }

            stopwatch1.Stop();
            Console.WriteLine($"Tempo de execução: {stopwatch1.Elapsed}");

            #endregion

            #region LeagueSeasons

            Stopwatch stopwatch2 = Stopwatch.StartNew();
            var leagueSeasons = leaguesSaved.SelectMany(leagueBd =>
            {
                var leagueResponse = leaguesResponse.FirstOrDefault(league => league.Name == leagueBd.Name);

                if (leagueResponse != null)
                    return leagueResponse.Season.Select(season => new LeagueSeasonModel(season, leagueBd.Code.Value));

                return Enumerable.Empty<LeagueSeasonModel>();
            }).ToList();

            await Task.Run(() => CreateSeasons(leagueSeasons));

            stopwatch2.Stop();
            Console.WriteLine($"Tempo de execução: {stopwatch2.Elapsed}");
            
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

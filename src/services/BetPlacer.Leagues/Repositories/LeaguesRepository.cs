using BetPlacer.Leagues.Database;
using BetPlacer.Leagues.Models;
using Microsoft.EntityFrameworkCore;

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

        public async void CreateOrUpdate(IEnumerable<LeagueModel> leagues)
        {
            foreach (var league in leagues)
            {
                var existentLeague = await _context.Leagues.FirstOrDefaultAsync(p => p.Code == league.Code);

                if (existentLeague == null)
                    _context.Leagues.Add(league);
                else
                    UpdateLeague(existentLeague, league);

            }
            
            await _context.SaveChangesAsync();
        }

        #region Private methods

        private void UpdateLeague(LeagueModel oldLeague, LeagueModel newLeague)
        {
            newLeague.Code = oldLeague.Code;
            _context.Entry(oldLeague).CurrentValues.SetValues(newLeague);
        }

        #endregion
    }
}

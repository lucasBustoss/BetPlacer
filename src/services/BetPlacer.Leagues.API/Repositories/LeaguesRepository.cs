using BetPlacer.Leagues.Config;
using BetPlacer.Leagues.API.Models;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Core.Models.Response.API.Leagues;
using BetPlacer.Leagues.API.Models.ValueObjects;

namespace BetPlacer.Leagues.API.Repositories
{
    public class LeaguesRepository : ILeaguesRepository
    {
        private readonly LeaguesDbContext _context;

        public LeaguesRepository(DbContextOptions<LeaguesDbContext> db)
        {
            _context = new LeaguesDbContext(db);
        }


        public IEnumerable<League> List(bool withSeason)
        {
            List<League> leaguesVO = new List<League>();
            var leagues = _context.Leagues.ToList();

            foreach (var league in leagues)
            {
                var seasons = new List<LeagueSeasonModel>();

                if (withSeason)
                {
                    var seasonsBd = _context.LeagueSeasons.Where(ls => ls.LeagueCode == league.Code);
                    seasons = seasonsBd.ToList();
                }

                leaguesVO.Add(new League(league, seasons));
            }

            return leaguesVO;
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
                {
                    var oldLeague = existingLeagues[leagueResponse.Name];
                    var newLeague = leagueModel;
                    newLeague.Code = oldLeague.Code;
                    _context.Entry(oldLeague).CurrentValues.SetValues(newLeague);
                }

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

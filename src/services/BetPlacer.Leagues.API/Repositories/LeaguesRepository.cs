﻿using BetPlacer.Leagues.Config;
using BetPlacer.Leagues.API.Models;
using Microsoft.EntityFrameworkCore;
using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using BetPlacer.Leagues.API.Models.ValueObjects;
using System.Threading;

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

        public IEnumerable<League> GetLeagueById(int leagueId)
        {
            List<League> leaguesVO = new List<League>();
            var league = _context.Leagues.Where(l => l.Code == leagueId).FirstOrDefault();

            var seasons = new List<LeagueSeasonModel>();

            var seasonsBd = _context.LeagueSeasons.Where(ls => ls.LeagueCode == league.Code);
            seasons = seasonsBd.ToList();

            leaguesVO.Add(new League(league, seasons));

            return leaguesVO;
        }

        public IEnumerable<League> GetLeaguesWithCurrentSeason()
        {
            List<League> newLeagues = new List<League>();
            var seasons = UpdateCurrentSeasons();

            var leagues = _context.Leagues.ToList();

            foreach (var league in leagues)
            {
                var leagueSeasons = seasons.Where(s => s.LeagueCode == league.Code).ToList();

                if (leagueSeasons.Count > 0)
                    newLeagues.Add(new League(league, leagueSeasons));
            }

            return newLeagues;
        }

        public void CreateOrUpdate(IEnumerable<LeaguesFootballResponseModel> leaguesResponse)
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

            _context.SaveChanges();

            #endregion

            #region LeagueSeasons

            var leagueSeasons = leaguesSaved.SelectMany(leagueBd =>
            {
                var leagueResponse = leaguesResponse.FirstOrDefault(league => league.Name == leagueBd.Name);

                if (leagueResponse != null)
                    return leagueResponse.Season.Select(season => new LeagueSeasonModel(season, leagueBd.Code.Value));

                return Enumerable.Empty<LeagueSeasonModel>();
            }).ToList();

            CreateSeasons(leagueSeasons);

            #endregion
        }

        #region Private methods

        private void CreateSeasons(List<LeagueSeasonModel> leagueSeasons)
        {
            foreach (var leagueSeason in leagueSeasons)
            {
                var leagueSeasonBd = _context.LeagueSeasons.FirstOrDefault(league => league.Code == leagueSeason.Code);

                if (leagueSeasonBd == null)
                {
                    _context.LeagueSeasons.Add(leagueSeason);
                    _context.SaveChanges();
                }
            }
        }

        private List<LeagueSeasonModel> UpdateCurrentSeasons()
        {
            string queryUpdateCurrent = @"
                    UPDATE league_seasons 
                        SET current = 
                            CASE 
                                WHEN EXISTS (
                                    SELECT 1
                                    FROM fixtures f
                                    WHERE f.season_code = league_seasons.code
                                    AND f.status = 'incomplete'
                                ) THEN TRUE
                                ELSE FALSE
                            END";

            _context.Database.ExecuteSqlRaw(queryUpdateCurrent);

            var seasons = _context.LeagueSeasons.ToList();

            foreach (var leagueSeason in seasons)
                _context.Entry(leagueSeason).Reload();

            return _context.LeagueSeasons.Where(ls => ls.Current).ToList();
        }

        #endregion
    }
}

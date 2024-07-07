using BetPlacer.Backtest.API.Models;
using BetPlacer.Core.API.Models.Request.PinnacleOdds;
using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.LeagueFixtureByDate;
using BetPlacer.Fixtures.API.Models;
using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.Enums;
using BetPlacer.Fixtures.API.Models.ValueObjects;

namespace BetPlacer.Fixtures.API.Repositories
{
    public interface IFixturesRepository
    {
        IEnumerable<Fixture> List(FixtureListSearchType searchType, IEnumerable<LeaguesApiResponseModel> leagues, IEnumerable<TeamsApiResponseModel> teams, bool withGoals, bool withStats, bool saveAsMessage, string backtestHash);
        IEnumerable<FixtureModel> GetFixturesWithoutOdds(LeaguesApiResponseModel league);
        List<int> GetFixtureCodesByDate(DateTime startDate, DateTime finalDate);
        List<LeagueFixtureByDate> ListFixturesByDate(IEnumerable<LeaguesApiResponseModel> leagues, IEnumerable<TeamsApiResponseModel> teams, IEnumerable<PunterBacktestFixture> fixturesStrategy);
        void CreateOrUpdateCompleteFixtures(IEnumerable<FixturesFootballResponseModel> fixturesResponse);
        List<string> CreateOrUpdateNextFixtures(IEnumerable<FixturesFootballResponseModel> fixturesResponse, List<PinnacleOddsModel> odds);
        void CreateOdds(FixtureOdds odds);
        void UpdateOdds(FixtureOdds odds);
        void CalculateFixtureStats(int leagueSeasonCode);
    }
}

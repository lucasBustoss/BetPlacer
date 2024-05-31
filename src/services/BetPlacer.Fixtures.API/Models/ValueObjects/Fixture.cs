using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Fixtures.API.Models.Entities;
using BetPlacer.Fixtures.API.Models.Entities.Trade;

namespace BetPlacer.Fixtures.API.Models.ValueObjects
{
    public class Fixture
    {
        public Fixture(FixtureModel fixture, LeaguesApiResponseModel leaguesResponseModel, TeamsApiResponseModel homeTeam, TeamsApiResponseModel awayTeam, List<FixtureGoalsModel> goals, FixtureStatsTradeModel stats)
        {
            Code = fixture.Code;
            Status = fixture.Status;
            LeagueName = leaguesResponseModel.Name;
            HomeTeamName = homeTeam.Name;
            AwayTeamName = awayTeam.Name;
            HomeTeamCode = homeTeam.Code;
            AwayTeamCode = awayTeam.Code;

            Goals = goals;
            Stats = stats;
        }

        public int Code { get; set; }
        public string Status { get; set; }
        public string LeagueName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int HomeTeamCode { get; set; }
        public int AwayTeamCode { get; set; }

        public List<FixtureGoalsModel> Goals { get; set; }
        public FixtureStatsTradeModel Stats { get; set; }
    }
}

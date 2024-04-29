using BetPlacer.Core.Models.Response.Microservice.Leagues;
using BetPlacer.Core.Models.Response.Microservice.Teams;
using BetPlacer.Fixtures.API.Models.Entities;

namespace BetPlacer.Fixtures.API.Models.ValueObjects
{
    public class Fixture
    {
        public Fixture(FixtureModel fixture, LeaguesApiResponseModel leaguesResponseModel, TeamsApiResponseModel homeTeam, TeamsApiResponseModel awayTeam)
        {
            Code = fixture.Code;
            Status = fixture.Status;
            LeagueName = leaguesResponseModel.Name;
            HomeTeamName = homeTeam.Name;
            AwayTeamName = awayTeam.Name; 
        }

        public int Code { get; set; }
        public string Status { get; set; }
        public string LeagueName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
    }
}

using BetPlacer.Core.Models.Response.Microservice.Leagues;

namespace BetPlacer.Fixtures.API.Models.ValueObjects.FixtureByDate
{
    public class LeagueFixtures
    {
        public LeagueFixtures(LeaguesApiResponseModel leagueModel)
        {
            LeagueCode = leagueModel.Code;
            LeagueName = leagueModel.Name;
            LeagueImageUrl = leagueModel.Image;
            LeagueCountry = leagueModel.Country;
            Fixtures = new List<FixtureDate>();
        }

        public int LeagueCode { get; set; }
        public string LeagueName { get; set; }
        public string LeagueImageUrl { get; set; }
        public string LeagueCountry { get; set; }
        public List<FixtureDate> Fixtures { get; set; }
    }
}

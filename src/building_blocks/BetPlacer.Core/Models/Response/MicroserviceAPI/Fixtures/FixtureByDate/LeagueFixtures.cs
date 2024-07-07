using BetPlacer.Core.Models.Response.Microservice.Leagues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.LeagueFixtureByDate
{
    public class LeagueFixtures
    {
        public LeagueFixtures()
        {
            
        }

        public LeagueFixtures(LeaguesApiResponseModel leagueModel)
        {
            LeagueCode = leagueModel.Code;
            LeagueName = leagueModel.Name;
            LeagueImageUrl = leagueModel.Image;
            LeagueCountry = leagueModel.Country;
            Fixtures = new List<FixtureDate>();
        }

        [JsonPropertyName("leagueCode")]
        public int LeagueCode { get; set; }
        
        [JsonPropertyName("leagueName")]
        public string LeagueName { get; set; }
        
        [JsonPropertyName("leagueImageUrl")]
        public string LeagueImageUrl { get; set; }
        
        [JsonPropertyName("leagueCountry")]
        public string LeagueCountry { get; set; }
        
        [JsonPropertyName("fixtures")]
        public List<FixtureDate> Fixtures { get; set; }
    }
}

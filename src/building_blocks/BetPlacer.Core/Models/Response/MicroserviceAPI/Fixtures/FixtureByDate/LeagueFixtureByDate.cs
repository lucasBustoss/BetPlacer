using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.LeagueFixtureByDate
{
    public class LeagueFixtureByDate
    {
        public LeagueFixtureByDate()
        {
            LeagueFixtures = new List<LeagueFixtures>();
        }

        [JsonPropertyName("date")]
        public string Date { get; set; }
        
        [JsonPropertyName("leagueFixtures")]
        public List<LeagueFixtures> LeagueFixtures { get; set; }
    }
}

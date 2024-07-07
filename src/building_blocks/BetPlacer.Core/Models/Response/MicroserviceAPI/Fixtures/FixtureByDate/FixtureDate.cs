using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.LeagueFixtureByDate
{
    public class FixtureDate
    {
        public FixtureDate()
        {
            
        }

        public FixtureDate(FixtureModel fixtureModel, FixtureStatsTradeModel stats, string filters, FixtureOdds odd, bool analyzedFixture)
        {
            Code = fixtureModel.Code;
            Date = fixtureModel.StartDate.AddHours(-3).ToString("dd/MM/yyyy HH:mm");
            HomeTeamName = fixtureModel.HomeTeamName;
            AwayTeamName = fixtureModel.AwayTeamName;
            FixtureOdds = odd;
            AnalyzedFixture = analyzedFixture;
            Filters = filters;

            if (stats != null)
            {
                Stats = stats;
                Stats.Fixture = null;
            }
        }

        [JsonPropertyName("code")]
        public int Code { get; set; }
        
        [JsonPropertyName("date")]
        public string Date { get; set; }
        
        [JsonPropertyName("homeTeamName")]
        public string HomeTeamName { get; set; }
        
        [JsonPropertyName("awayTeamName")]
        public string AwayTeamName { get; set; }
        
        [JsonPropertyName("filters")]
        public string Filters { get; set; }
        
        [JsonPropertyName("analyzedFixture")]
        public bool AnalyzedFixture { get; set; }
        
        [JsonPropertyName("fixtureOdds")]
        public FixtureOdds FixtureOdds { get; set; }

        [JsonPropertyName("informedOdds")]
        public bool InformedOdds
        {
            get
            {
                return
                    FixtureOdds != null &&
                    FixtureOdds.HomeOdd > 0 &&
                    FixtureOdds.DrawOdd > 0 &&
                    FixtureOdds.AwayOdd > 0 &&
                    FixtureOdds.Over25Odd > 0 &&
                    FixtureOdds.Under25Odd > 0 &&
                    FixtureOdds.BTTSYesOdd > 0 &&
                    FixtureOdds.BTTSNoOdd > 0;
            }
        }

        [JsonPropertyName("stats")]
        public FixtureStatsTradeModel Stats { get; set; }
    }
}

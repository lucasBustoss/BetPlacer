using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Request
{
    public class FixtureOddsRequest
    {
        [JsonPropertyName("fixtureCode")]
        public int FixtureCode { get; set; }

        [JsonPropertyName("oddHome")]
        public double OddHome { get; set; }

        [JsonPropertyName("oddDraw")]
        public double OddDraw { get; set; }

        [JsonPropertyName("oddAway")]
        public double OddAway { get; set; }

        [JsonPropertyName("oddOver25")]
        public double OddOver25 { get; set; }

        [JsonPropertyName("oddUnder25")]
        public double OddUnder25 { get; set; }

        [JsonPropertyName("oddBttsYes")]
        public double OddBttsYes { get; set; }

        [JsonPropertyName("oddBttsNo")]
        public double OddBttsNo { get; set; }
    }
}

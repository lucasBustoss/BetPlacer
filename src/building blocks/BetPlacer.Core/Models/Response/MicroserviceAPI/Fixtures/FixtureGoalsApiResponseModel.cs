using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures
{
    public class FixtureGoalsApiResponseModel
    {
        [JsonPropertyName("minute")]
        public string Minute { get; set; }

        [JsonPropertyName("teamId")]
        public int TeamId { get; set; }
    }
}

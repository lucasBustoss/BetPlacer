using System.Text.Json.Serialization;

namespace BetPlacer.Teams.API.Models.ValueObjects
{
    public class Team
    {
        public Team(TeamModel teamModel)
        {
            Code = teamModel.Code;
            Name = teamModel.Name;
            Country = teamModel.Country;
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}

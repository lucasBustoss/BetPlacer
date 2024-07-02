using BetPlacer.Core.Models.Response.FootballAPI.Teams;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Teams.API.Models
{
    public class TeamModel
    {
        public TeamModel() { }

        public TeamModel(TeamsFootballResponseModel teamResponseModel)
        {
            Code = teamResponseModel.Id;
            Name = teamResponseModel.Name.Replace("'", "");
            Country = teamResponseModel.Country;
        }

        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}

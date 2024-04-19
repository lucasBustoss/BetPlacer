using BetPlacer.Core.Models.Response.API.Teams;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Teams.API.Models
{
    public class TeamModel
    {
        public TeamModel() { }

        public TeamModel(TeamsResponseModel teamResponseModel)
        {
            Code = teamResponseModel.Id;
            Name = teamResponseModel.Name;
            Country = teamResponseModel.Country;
        }

        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}

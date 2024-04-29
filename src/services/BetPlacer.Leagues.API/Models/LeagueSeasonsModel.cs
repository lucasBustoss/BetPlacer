using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Leagues.API.Models
{
    public class FixtureGoalsModel
    {
        public FixtureGoalsModel() { }

        public FixtureGoalsModel(LeagueSeasonResponseModel leagueSeasonResponseModel, int leagueCode)
        {
            var rawYear = leagueSeasonResponseModel.Year.ToString();

            Code = leagueSeasonResponseModel.Id;
            LeagueCode = leagueCode;
            Year = rawYear.Length > 4 ? $"{rawYear.Substring(0, 4)}-{rawYear.Substring(4, 4)}" : rawYear;
        }
        
        public LeagueModel League { get; set; }

        [Key]
        public int Code { get; set; }
        public int LeagueCode { get; set; }
        public string Year { get; set; }
    }
}

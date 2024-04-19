using BetPlacer.Leagues.API.Models;

namespace BetPlacer.Leagues.API.Models.ValueObjects
{
    public class League
    {
        public League() { }

        public League(LeagueModel leagueModel, List<LeagueSeasonModel> leagueSeasonModel)
        {
            Code = leagueModel.Code.Value;
            Name = leagueModel.Name;
            Country = leagueModel.Country;
            ImageUrl = leagueModel.ImageUrl;

            if (leagueSeasonModel != null)
            {
                Seasons = leagueSeasonModel;
                Seasons.ForEach(season => season.League = null);
            }

        }

        public int Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public List<LeagueSeasonModel> Seasons { get; set; }
    }
}

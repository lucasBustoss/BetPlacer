namespace BetPlacer.Fixtures.API.Models.RequestModel
{
    public class FixturesRequestModel
    {
        public int? LeagueSeasonCode { get; set; }

        public bool IsValid()
        {
            return LeagueSeasonCode != null && LeagueSeasonCode.Value > 0;
        }
    }
}

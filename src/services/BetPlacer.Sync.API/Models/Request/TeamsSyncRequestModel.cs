namespace BetPlacer.Teams.API.Controllers.RequestModel
{
    public class TeamsSyncRequestModel
    {
        public TeamsSyncRequestModel(int leagueSeasonCode)
        {
            LeagueSeasonCode = leagueSeasonCode;
        }

        public int? LeagueSeasonCode { get; set; }

        public bool IsValid()
        {
            return LeagueSeasonCode != null && LeagueSeasonCode.Value > 0;
        }
    }
}

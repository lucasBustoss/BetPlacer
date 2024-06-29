namespace BetPlacer.Punter.API.Models.ValueObjects.Match.Team
{
    public class TeamMatchesBySeason
    {
        public string TeamName { get; set; }
        public string Season { get; set; }
        public List<MatchBaseData> Matches { get; set; }
    }
}

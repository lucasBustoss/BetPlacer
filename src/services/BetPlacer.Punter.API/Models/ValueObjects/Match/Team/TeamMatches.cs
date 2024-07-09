namespace BetPlacer.Punter.API.Models.ValueObjects.Match.Team
{
    public class TeamMatches
    {
        public string TeamName { get; set; }
        public List<MatchBaseData> Matches { get; set; }
    }
}

namespace BetPlacer.Fixtures.API.Models.ValueObjects.FixtureByDate
{
    public class LeagueFixtureByDate
    {
        public LeagueFixtureByDate()
        {
            LeagueFixtures = new List<LeagueFixtures>();
        }

        public string Date { get; set; }
        public List<LeagueFixtures> LeagueFixtures { get; set; }
    }
}

using BetPlacer.Fixtures.API.Models.Entities;

namespace BetPlacer.Fixtures.API.Models.ValueObjects.FixtureByDate
{
    public class FixtureDate
    {
        public FixtureDate(FixtureModel fixtureModel)
        {
            Code = fixtureModel.Code;
            Date = fixtureModel.StartDate.AddHours(-3).ToString("dd/MM/yyyy HH:mm");
            HomeTeamName = fixtureModel.HomeTeamName;
            AwayTeamName = fixtureModel.AwayTeamName;
            OddHome = 2;
            OddDraw = 2;
            OddAway = 2;
        }

        public int Code { get; set; }
        public string Date { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public double OddHome { get; set; }
        public double OddDraw { get; set; }
        public double OddAway { get; set; }
    }
}

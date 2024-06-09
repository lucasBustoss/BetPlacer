using BetPlacer.Backtest.API.Models.Entities.Odds;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class BacktestModel
    {
        public BacktestModel()
        {
            
        }

        public BacktestModel(bool createData)
        {
            if (createData)
            {
                CreationDate = DateTime.UtcNow;
                UserId = 1;
                Filters = new List<BacktestFilterModel>();
                Leagues = new List<LeagueBacktestModel>();
                LeagueSeasons = new List<LeagueSeasonBacktestModel>();
                Teams = new List<TeamBacktestModel>();
            }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Type { get; set; }
        public int TeamType { get; set; }
        public double FilteredFixtures { get; set; }
        public double MatchedFixtures { get; set; }
        public int MaxGoodRun { get; set; }
        public int MaxBadRun { get; set; }
        public bool UsesInFixture { get; set; }
        public List<BacktestFilterModel> Filters { get; set; }
        public List<LeagueBacktestModel> Leagues { get; set; }
        public List<LeagueSeasonBacktestModel> LeagueSeasons { get; set; }
        public List<TeamBacktestModel> Teams { get; set; }
    }
}

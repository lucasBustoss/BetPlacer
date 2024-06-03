using BetPlacer.Core.Models.Response.Microservice.Leagues;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class LeagueBacktestModel
    {
        public LeagueBacktestModel() { }

        public LeagueBacktestModel(LeaguesApiResponseModel league, double leagueRatio)
        {
            LeagueCode = league.Code;
            LeagueName = league.Name;
            LeagueRatio = leagueRatio;
        }

        public BacktestModel Backtest { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int BacktestCode { get; set; }
        public int LeagueCode { get; set; }
        public string LeagueName { get; set; }
        public double LeagueRatio { get; set; }
    }
}

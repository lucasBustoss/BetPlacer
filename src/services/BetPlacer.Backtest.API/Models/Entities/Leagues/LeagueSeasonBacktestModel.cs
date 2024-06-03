using BetPlacer.Core.Models.Response.Microservice.Leagues;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class LeagueSeasonBacktestModel
    {
        public LeagueSeasonBacktestModel()
        {
            
        }

        public LeagueSeasonBacktestModel(LeaguesApiResponseModel league, LeagueSeasonApiResponseModel season, double ratio)
        {
            LeagueCode = league.Code;
            LeagueName = league.Name;
            LeagueSeasonCode = season.Code;
            LeagueSeasonYear = season.Year;
            LeagueSeasonRatio = ratio;
        }

        public BacktestModel Backtest { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int BacktestCode { get; set; }
        public int LeagueCode { get; set; }
        public int LeagueSeasonCode { get; set; }
        public string LeagueName { get; set; }
        public string LeagueSeasonYear { get; set; }
        public double LeagueSeasonRatio { get; set; }
    }
}

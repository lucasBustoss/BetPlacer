using BetPlacer.Core.Models.Response.Microservice.Teams;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BetPlacer.Backtest.API.Models.Entities
{
    public class TeamBacktestModel
    {
        public TeamBacktestModel()
        {
            
        }

        public TeamBacktestModel(TeamsApiResponseModel team, double ratio)
        {
            TeamCode = team.Code;
            TeamName = team.Name;
            TeamRatio = ratio;
        }

        public BacktestModel Backtest { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public int BacktestCode { get; set; }
        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public double TeamRatio { get; set; }
    }
}

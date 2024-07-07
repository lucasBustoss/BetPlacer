using BetPlacer.Core.Models.Response.FootballAPI.Fixtures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures.Entities
{
    public class FixtureModel
    {
        public FixtureModel() { }

        public FixtureModel(FixturesFootballResponseModel fixtureResponseModel)
        {
            Code = fixtureResponseModel.Code;
            SeasonCode = fixtureResponseModel.LeagueSeasonCode;
            StartDate = DateTimeOffset.FromUnixTimeSeconds(fixtureResponseModel.DateTimestamp).UtcDateTime;
            Status = fixtureResponseModel.Status;
            HomeTeamId = fixtureResponseModel.HomeTeamId;
            AwayTeamId = fixtureResponseModel.AwayTeamId;
            HomeTeamName = fixtureResponseModel.HomeTeamName;
            AwayTeamName = fixtureResponseModel.AwayTeamName;
            HomeTeamImage = fixtureResponseModel.HomeTeamImage;
            AwayTeamImage = fixtureResponseModel.AwayTeamImage;
            HomeTeamGoals = fixtureResponseModel.HomeGoals;
            AwayTeamGoals = fixtureResponseModel.AwayGoals;
            HomeTeamPPG = fixtureResponseModel.HomePointsPerGame;
            AwayTeamPPG = fixtureResponseModel.AwayPointsPerGame;
            HomeTeamXG = fixtureResponseModel.HomeTeamXG;
            AwayTeamXG = fixtureResponseModel.AwayTeamXG;
        }

        [Key]
        public int Code { get; set; }
        public int SeasonCode { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamImage { get; set; }
        public string AwayTeamImage { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public double HomeTeamPPG { get; set; }
        public double AwayTeamPPG { get; set; }
        public double HomeTeamXG { get; set; }
        public double AwayTeamXG { get; set; }
    }
}

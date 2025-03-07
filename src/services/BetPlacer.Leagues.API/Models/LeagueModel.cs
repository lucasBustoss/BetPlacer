﻿using BetPlacer.Core.Models.Response.FootballAPI.Leagues;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetPlacer.Leagues.API.Models
{
    public class LeagueModel
    {
        public LeagueModel() { }

        public LeagueModel(LeaguesFootballResponseModel leagueResponseModel)
        {
            Name = leagueResponseModel.Name;
            Country = leagueResponseModel.Country;
            ImageUrl = leagueResponseModel.Image;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
    }
}

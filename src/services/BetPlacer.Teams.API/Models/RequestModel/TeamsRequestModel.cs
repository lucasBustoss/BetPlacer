﻿namespace BetPlacer.Teams.API.Models.RequestModel
{
    public class TeamsRequestModel
    {
        public int? LeagueSeasonCode { get; set; }

        public bool IsValid()
        {
            return LeagueSeasonCode != null && LeagueSeasonCode.Value > 0;
        }
    }
}

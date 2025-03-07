﻿using BetPlacer.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using BetPlacer.Leagues.API.Repositories;
using System.Text.Json;
using BetPlacer.Core.Models.Response.Core;
using BetPlacer.Core.Models.Response.FootballAPI.Leagues;

namespace BetPlacer.Leagues.Controllers
{
    [Route("api/leagues")]
    public class LeaguesController : BaseController
    {
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public LeaguesController(ILeaguesRepository leaguesRepository, IConfiguration configuration)
        {
            _leaguesRepository = leaguesRepository;

            _httpClient = new HttpClient();

            #region EnvironmentVariable

            var coreApiAddress = Environment.GetEnvironmentVariable("BETPLACER_CoreApiAddress") ?? configuration["BetPlacer:CoreApiAddress"];
            if (string.IsNullOrEmpty(coreApiAddress))
                throw new Exception("A variável de ambiente BETPLACER_CoreApiAddress não está definida.");

            _apiUrl = coreApiAddress;

            #endregion

            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiUrl) };

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        [HttpGet]
        public ActionResult GetLeagues(bool? withSeasons)
        {
            try
            {
                bool getLeaguesWithSeason = withSeasons != null ? withSeasons.Value : false;
                var leagues = _leaguesRepository.List(getLeaguesWithSeason);
                return OkResponse(leagues.OrderBy(l => l.Name).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetLeagues(int id)
        {
            var league = _leaguesRepository.GetLeagueById(id);
            return OkResponse(league);
        }

        [HttpGet("season/current")]
        public ActionResult GetLeaguesWithCurrentSeasons()
        {
            var leagues = _leaguesRepository.GetLeaguesWithCurrentSeason();

            return OkResponse(leagues);
        }

        [HttpPost]
        public async Task<ActionResult> SyncLeagues()
        {
            try
            {
                var request = await _httpClient.GetAsync("leagues?chosen_leagues_only=true");

                if (request.IsSuccessStatusCode)
                {
                    var responseLeaguesString = await request.Content.ReadAsStringAsync();
                    BaseCoreResponseModel<LeaguesFootballResponseModel> response = JsonSerializer.Deserialize<BaseCoreResponseModel<LeaguesFootballResponseModel>>(responseLeaguesString);

                    _leaguesRepository.CreateOrUpdate(response.Data);

                    var leagues = _leaguesRepository.List(true);

                    return OkResponse(leagues);
                }
                //else
                {
                    var errorMessage = JsonSerializer.Deserialize<object>(await request.Content.ReadAsStringAsync());
                    Console.WriteLine(errorMessage);
                    Console.WriteLine(request.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }
    }
}

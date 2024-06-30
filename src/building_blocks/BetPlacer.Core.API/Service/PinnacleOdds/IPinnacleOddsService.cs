using BetPlacer.Core.API.Models.Request.PinnacleOdds;

namespace BetPlacer.Core.API.Service.PinnacleOdds
{
    public interface IPinnacleOddsService
    {
        Task<List<PinnacleOddsModel>> GetOdds(int leagueCode);
    }
}

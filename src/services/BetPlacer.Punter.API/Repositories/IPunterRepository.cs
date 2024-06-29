using BetPlacer.Punter.API.Models.ValueObjects.Strategy;

namespace BetPlacer.Punter.API.Repositories
{
    public interface IPunterRepository
    {
        void Create(int leagueCode, List<StrategyInfo> strategies);
    }
}

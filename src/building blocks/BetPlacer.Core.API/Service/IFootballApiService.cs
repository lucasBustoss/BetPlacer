namespace BetPlacer.Core.API.Service
{
    public interface IFootballApiService
    {
        public Task<string> GetLeagues();
    }
}

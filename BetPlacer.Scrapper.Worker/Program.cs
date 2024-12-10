using BetPlacer.Scrapper.Worker.Services;

namespace BetPlacer.Scrapper.Worker
{
    public class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.oddsportal.com/football/spain/laliga2/results/";
            ScrapperService service = new ScrapperService(url);

            service.GetMatches();
        }
    }
}

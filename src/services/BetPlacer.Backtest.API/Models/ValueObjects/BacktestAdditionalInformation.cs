namespace BetPlacer.Backtest.API.Models.ValueObjects
{
    public class BacktestAdditionalInformation
    {
        public BacktestAdditionalInformation(string info)
        {
            Info = info;
        }

        public string Info { get; set; }
    }
}

namespace BetPlacer.Backtest.API.Messages.Consumer
{
    public interface IMessageConsumer
    {
        void StartConsuming();
        bool IsFinished { get; }
    }
}

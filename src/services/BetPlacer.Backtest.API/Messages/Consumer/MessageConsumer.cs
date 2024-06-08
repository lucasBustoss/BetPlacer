using BetPlacer.Backtest.API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BetPlacer.Backtest.API.Messages.Consumer
{
    public class MessageConsumer : BackgroundService, IMessageConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ICalculateBacktest _calculate;
        private bool _isConsuming;
        private string _backtestHash;
        private static bool _queueDeleted = false;
        private static readonly object _lock = new object();
        private string _queueName;

        public bool IsFinished { get; private set; }

        public MessageConsumer(ICalculateBacktest calculate, string backtestHash)
        {
            _calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare($"backtest_{backtestHash}", false, false, false, null);
            _backtestHash = backtestHash;

            _queueName = $"backtest_{backtestHash}";
            Console.WriteLine("Consumer created and connected to RabbitMQ.");
        }

        public void StartConsuming()
        {
            _isConsuming = true;
            IsFinished = false;
            Console.WriteLine("Started consuming...");
        }

        public void StopConsuming()
        {
            _isConsuming = false;
            Console.WriteLine("Stopped consuming...");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_connection == null || !_connection.IsOpen)
            {
                Console.WriteLine("Connection is not open.");
                return;
            }

            if (_channel == null || !_channel.IsOpen)
            {
                Console.WriteLine("Channel is not open.");
                return;
            }

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evt) =>
            {
                if (!_isConsuming)
                {
                    Console.WriteLine("Consuming paused.");
                    return;
                }

                var content = Encoding.UTF8.GetString(evt.Body.ToArray());

                if (IsEndOfMessagesMessage(content))
                {
                    IsFinished = true;
                    StopConsuming();

                    _channel.BasicAck(evt.DeliveryTag, false);

                    return;
                }

                var message = JsonSerializer.Deserialize<FixtureMessage>(content);

                if (message.Fixture != null)
                {
                    _calculate.CalculateFixture(message.Fixture);
                    Console.WriteLine("Processed fixture.");

                    _channel.BasicAck(evt.DeliveryTag, false);
                }
            };

            _channel.BasicConsume($"backtest_{_backtestHash}", false, consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken); // Check every second
            }
        }

        public void DeleteQueue()
        {
            lock (_lock)
            {
                if (!_queueDeleted && _channel != null && _channel.IsOpen)
                {
                    _channel.QueueDelete(_queueName);
                    _queueDeleted = true;
                    Console.WriteLine("Queue deleted.");
                }
            }
        }

        private bool IsEndOfMessagesMessage(string message)
        {
            try
            {
                var endOfMessages = JsonSerializer.Deserialize<EndOfMessagesMessage>(message);
                return endOfMessages.IsFinished;
            }
            catch (JsonException)
            {
                // If deserialization fails, it is not an end-of-messages message
                return false;
            }
        }

        public override void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
using BetPlacer.Core.Messages.PlanningBet.Core.Integration;
using BetPlacer.Fixtures.API.Messages.ModelToMessage;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace BetPlacer.Fixtures.API.Messages
{
    public class MessageSender : IMessageSender, IDisposable
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private IModel _channel;
        private bool _disposed;

        public MessageSender()
        {
            _hostName = "localhost";
            _userName = "admin";
            _password = "123";
            _connection = CreateConnection();
            _channel = _connection.CreateModel();
        }

        private IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
            };
            return factory.CreateConnection();
        }

        public void SendMessage<T>(BaseMessage message, string queueName)
        {
            EnsureConnectionAndChannel();

            _channel.QueueDeclare(queueName, false, false, false, null);
            byte[] body = GetMessageAsByteArray<T>(message);

            bool messageSent = false;
            int retryCount = 0;
            int maxRetries = 5;

            while (!messageSent && retryCount < maxRetries)
            {
                try
                {
                    _channel.BasicPublish("", queueName, null, body);
                    messageSent = true;
                }
                catch (Exception ex) when (ex is BrokerUnreachableException || ex is EndOfStreamException)
                {
                    retryCount++;
                    Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retryCount))).Wait();
                    if (retryCount == maxRetries)
                    {
                        throw new Exception("Failed to send message after multiple retries.", ex);
                    }
                }
            }
        }

        private byte[] GetMessageAsByteArray<T>(BaseMessage message)
        {
            message.MessageCreated = DateTime.Now;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Configuração para camelCase
            };
            var json = JsonSerializer.Serialize((T)(object)message, options);
            return Encoding.UTF8.GetBytes(json);
        }

        private void EnsureConnectionAndChannel()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = CreateConnection();
            }

            if (_channel == null || !_channel.IsOpen)
            {
                _channel = _connection.CreateModel();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _channel?.Close();
                    _channel?.Dispose();
                    _connection?.Close();
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }

        public void SendEndOfMessagesSignal(string queueName)
        {
            var endOfMessages = new EndOfMessagesMessage();
            SendMessage<EndOfMessagesMessage>(endOfMessages, queueName);
        }
    }
}

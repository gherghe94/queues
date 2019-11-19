using System;
using System.Text;
using NATS.Client;
using Newtonsoft.Json;

namespace Queues.Desktop.Services
{
    public class NatsService : IDisposable
    {
        private readonly IConnection _natsConnection;

        private bool _disposed;

        public event EventHandler<UserCreatedMessageEventArgs> UserCreated;

        public NatsService()
        {
            _natsConnection = new ConnectionFactory().CreateConnection("nats://127.0.0.1:4223");
        }

        public void StartListening()
        {
            var asyncSubscription = _natsConnection.SubscribeAsync("receive-employees");
            asyncSubscription.MessageHandler += OnMessageReceived;
            asyncSubscription.Start();
        }

        public void StopListening()
        {
            _natsConnection.Close();
        }

        private void OnMessageReceived(object sender, MsgHandlerEventArgs e)
        {
            var rawMessage = e.Message;
            var newUser = JsonConvert.DeserializeObject<UserCreatedMessage>(Encoding.UTF8.GetString(rawMessage.Data));
            UserCreated(this, new UserCreatedMessageEventArgs(newUser));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                StopListening();
            }

            _disposed = true;
        }
    }
}

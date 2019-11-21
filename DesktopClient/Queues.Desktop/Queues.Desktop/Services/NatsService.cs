using System;
using System.Text;
using NATS.Client;
using Newtonsoft.Json;

namespace Queues.Desktop.Services
{
    public abstract class NatsService : IDisposable
    {
        private readonly IConnection _natsConnection;

        private bool _disposed;

        protected abstract string GetSubject();

        protected abstract void OnMessageReceived(object sender, MsgHandlerEventArgs e);

        public NatsService()
        {
            _natsConnection = new ConnectionFactory().CreateConnection("nats://127.0.0.1:4223");
        }

        public void StartListening()
        {
            var subject = GetSubject();
            var asyncSubscription = _natsConnection.SubscribeAsync(subject);

            asyncSubscription.MessageHandler += OnMessageReceived;
            asyncSubscription.Start();
        }

        public void StopListening()
        {
            _natsConnection.Close();
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

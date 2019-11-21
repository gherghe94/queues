using NATS.Client;
using Newtonsoft.Json;
using Queues.Desktop.Services.CustomEventArgs;
using Queues.Desktop.Services.Messages;
using System;
using System.Text;

namespace Queues.Desktop.Services
{
    public class OrderReceiverService : NatsService
    {
        public event EventHandler<OrderCreatedMessageEventArgs> OrderCreated;

        protected override string GetSubject()
        {
            return "receive-order";
        }

        protected override void OnMessageReceived(object sender, MsgHandlerEventArgs e)
        {
            var rawMessage = e.Message;
            var newOrder = JsonConvert.DeserializeObject<OrderCreatedMessage>(Encoding.UTF8.GetString(rawMessage.Data));

            OrderCreated(this, new OrderCreatedMessageEventArgs(newOrder));
        }
    }
}

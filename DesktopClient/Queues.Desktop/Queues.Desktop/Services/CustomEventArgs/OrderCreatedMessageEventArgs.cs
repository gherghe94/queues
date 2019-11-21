using Queues.Desktop.Services.Messages;

namespace Queues.Desktop.Services.CustomEventArgs
{
    public class OrderCreatedMessageEventArgs
    {
        public OrderCreatedMessage Order { get; }

        public OrderCreatedMessageEventArgs(OrderCreatedMessage newOrder)
        {
            Order = newOrder;
        }
    }
}
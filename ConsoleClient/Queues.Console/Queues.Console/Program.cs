namespace Queues.Console
{
    using NATS.Client;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory();
            var connection = connectionFactory.CreateConnection("nats://127.0.0.1:4223");
            Console.WriteLine("Connection to NATS has been established.");
            EventHandler<MsgHandlerEventArgs> handler = (sender, vargs) =>
            {
                Console.WriteLine(vargs.Message);
                // vargs.Message.ArrivalSubcription.Unsubscribe(); // just if we want to!
            };

            var asyncSubscription = connection.SubscribeAsync("receive-user");
            asyncSubscription.MessageHandler += handler;
            asyncSubscription.Start();

            var asyncSubscriptionOrder = connection.SubscribeAsync("receive-order");
            asyncSubscriptionOrder.MessageHandler += handler;
            asyncSubscriptionOrder.Start();

            Console.WriteLine("Async subscription has been made on 'receive-user' channel and also on 'receive-orders'");
            Console.WriteLine("Listening ...");
        }
    }
}

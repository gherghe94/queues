namespace Queues.Console
{
    using NATS.Client;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory();
            var connection = connectionFactory.CreateConnection();
            EventHandler<MsgHandlerEventArgs> handler = (sender, vargs) =>
            {
                Console.WriteLine(vargs.Message);
                // vargs.Message.ArrivalSubcription.Unsubscribe(); // just if we want to!
            };

            var asyncSubscription = connection.SubscribeAsync("receive-employees");
            asyncSubscription.MessageHandler += handler;
            asyncSubscription.Start();
        }
    }
}

using System;

namespace FireOnWheels.Services.Notification
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var rabbitMqManager = new RabbitMQManager())
            {
                rabbitMqManager.ListenForNotificationOrderQueue();
                Console.WriteLine("Notification service running... Press any key to exit...");

                Console.ReadKey();
            }
        }
    }
}

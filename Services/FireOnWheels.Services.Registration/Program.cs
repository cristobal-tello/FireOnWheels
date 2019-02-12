using FireOnWheels;
using System;

namespace FireOnWheels.Services.RegisterOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var rabbitMqManager = new RabbitMQManager())
            {
                rabbitMqManager.ListenForRegisterOrderQueue();
                Console.WriteLine("RegisterOrder service running... Press any key to exit...");

                Console.ReadKey();
            }
        }
    }
}

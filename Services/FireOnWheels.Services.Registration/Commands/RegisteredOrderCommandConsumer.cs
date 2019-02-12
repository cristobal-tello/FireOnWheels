using FireOnWheels.Business.Commands;
using FireOnWheels.Business.Constants;
using FireOnWheels.Business.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace FireOnWheels.Services.RegisterOrder.Commands
{
    public  class RegisteredOrderCommandConsumer : DefaultBasicConsumer
    {
        private readonly RabbitMQManager rabbitMQManager;

        public RegisteredOrderCommandConsumer(RabbitMQManager rabbitMQManager)
        {
            this.rabbitMQManager = rabbitMQManager;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            if (properties.ContentType != RabbitMQConstants.JsonMimeType)
            {
                throw new ArgumentException("Error");
            }

            var message = Encoding.UTF8.GetString(body);
            var commandObj = JsonConvert.DeserializeObject<RegisterOrderCommand>(message);

            this.Consumer(commandObj);

            rabbitMQManager.SendAck(deliveryTag);
        }

        private void Consumer(IRegisterOrderCommand command)
        {
            Random rnd = new Random();
            var id = rnd.Next(1, 13);

            Console.WriteLine($"Order with id: {id} registered");
            Console.WriteLine(command.ToString());

            var notificationOrderCommand = new NotificationOrderCommand(){ OrderId = id};
            rabbitMQManager.SendNotificationOrderCommand(notificationOrderCommand);
        }
    }
}
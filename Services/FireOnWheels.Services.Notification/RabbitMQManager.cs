
using FireOnWheels.Business.Commands;
using FireOnWheels.Business.Constants;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace FireOnWheels.Services.Notification
{
    public class RabbitMQManager : IDisposable
    {
        private readonly IModel channel;

        public RabbitMQManager()
        {
            var connectionFactory = new ConnectionFactory { Uri = new Uri(RabbitMQConstants.RabbitMqUri) } ;
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void ListenForNotificationOrderQueue()
        {
            channel.QueueDeclare(RabbitMQConstants.NotificationOrderQueue, false, false, false, null);
            channel.BasicQos(0, 1, false);

            var eventingConsumer = new EventingBasicConsumer(channel);
            eventingConsumer.Received += (chan, args) =>
            {
                var contentType = args.BasicProperties.ContentType;
                if (contentType != RabbitMQConstants.JsonMimeType)
                {
                    throw new ArgumentException("error");
                }

                var message = Encoding.UTF8.GetString(args.Body);
                var command = JsonConvert.DeserializeObject<NotificationOrderCommand>(message);

                var consumer = new NotificationOrderCommandConsumer();
                consumer.Consume(command);

                channel.BasicAck(args.DeliveryTag, false);
            };
            channel.BasicConsume(RabbitMQConstants.NotificationOrderQueue, false, eventingConsumer);
        }
        
        public void Dispose()
        {
            if (!channel.IsClosed) {
                channel.Close();
             }
        }
    }
}

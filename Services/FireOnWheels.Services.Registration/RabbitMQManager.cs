using FireOnWheels.Business.Constants;
using FireOnWheels.Business.Interfaces;
using FireOnWheels.Services.RegisterOrder.Commands;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace FireOnWheels.Services.RegisterOrder
{
    public class RabbitMQManager : IDisposable          // TO-DO : Refactor, Created RabbitMQManagerBase and improve IDisposable
    {
        private readonly IModel channel;

        public RabbitMQManager()
        {
            var connectionFactory = new ConnectionFactory { Uri = new Uri(RabbitMQConstants.RabbitMqUri) } ;
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        }
        public void ListenForRegisterOrderQueue()
        {
            channel.QueueDeclare(RabbitMQConstants.RegisterOrderServiceQueue, false, false, false, null);
            channel.BasicQos(0, 1, false);

            var consumer = new RegisteredOrderCommandConsumer(this);
            channel.BasicConsume(RabbitMQConstants.RegisterOrderServiceQueue, false, consumer);
        }
        public void SendNotificationOrderCommand(INotificationOrderCommand command)
        {
            channel.ExchangeDeclare(RabbitMQConstants.NotificationOrderExchange, ExchangeType.Fanout);
            channel.QueueDeclare(RabbitMQConstants.NotificationOrderQueue, false, false, false, null);
            channel.QueueBind(RabbitMQConstants.NotificationOrderQueue, RabbitMQConstants.NotificationOrderExchange, string.Empty);

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = RabbitMQConstants.JsonMimeType;

            channel.BasicPublish(RabbitMQConstants.NotificationOrderExchange, string.Empty, messageProperties, Encoding.UTF8.GetBytes(serializedCommand));
        }
        public void SendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag, false);
        }        
        public void Dispose()
        {
            if (!channel.IsClosed) {
                channel.Close();
             }
        }
    }
}

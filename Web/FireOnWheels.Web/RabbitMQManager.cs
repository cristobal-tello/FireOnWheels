using FireOnWheels.Business.Constants;
using FireOnWheels.Business.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace FireOnWheels.Web
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

        public void SendRegisterOrderCommand(IRegisterOrderCommand command)
        {
            channel.ExchangeDeclare(RabbitMQConstants.RegisterOrderExchange, ExchangeType.Direct);
            channel.QueueDeclare(RabbitMQConstants.RegisterOrderServiceQueue, false, false, false, null);
            channel.QueueBind(RabbitMQConstants.RegisterOrderServiceQueue, RabbitMQConstants.RegisterOrderExchange, string.Empty);

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = RabbitMQConstants.JsonMimeType;

            channel.BasicPublish(RabbitMQConstants.RegisterOrderExchange, string.Empty, messageProperties, Encoding.UTF8.GetBytes(serializedCommand));
        }

        public void Dispose()
        {
            if (!channel.IsClosed) {
                channel.Close();
             }
        }
    }
}

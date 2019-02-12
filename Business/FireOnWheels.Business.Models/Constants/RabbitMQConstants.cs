namespace FireOnWheels.Business.Constants
{
    public static class RabbitMQConstants
    {
        public const string RabbitMqUri = "amqp://guest:guest@localhost:5672/";

        public const string JsonMimeType = "application/json";
        
        public const string UserName = "guest";
        public const string Password = "guest";
        
        public const string RegisterOrderExchange = "fireonwheels.registerorder.exchange";
        public const string RegisterOrderServiceQueue = "fireonwheels.registerorder.queue";

        public const string NotificationOrderExchange = "fireonwheels.orderregistered.exchange";
        public const string NotificationOrderQueue = "fireonwheels.orderregistered.notification.queue";
    }
}

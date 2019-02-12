using FireOnWheels.Business.Interfaces;
using System;

namespace FireOnWheels.Services.Notification
{
    public class NotificationOrderCommandConsumer
    {
        public void Consume(INotificationOrderCommand command)
        { 
            Console.WriteLine($"Notification receivied. Order Id: {command.OrderId}");
        }
    }
}
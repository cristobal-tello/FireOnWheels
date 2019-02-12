using FireOnWheels.Business.Interfaces;

namespace FireOnWheels.Business.Commands
{
    public class NotificationOrderCommand : INotificationOrderCommand
    {
        public int OrderId { get; set; }
    }
}
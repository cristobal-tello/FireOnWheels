using FireOnWheels.Business.Interfaces;
using System;

namespace FireOnWheels.Business.Commands
{
    public class RegisterOrderCommand : IRegisterOrderCommand
    {
        public Guid CorrelationId { get; set; }
        public string PickupName { get; set; }
        public string PickupAddress { get; set; }
        public string PickupCity { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public override string ToString()
        {
            return string.Format($"PickupName:{PickupName}\r\nPickupaddress:{PickupAddress}");
        }
    }
}

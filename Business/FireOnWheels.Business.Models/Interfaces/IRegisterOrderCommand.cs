using System;

namespace FireOnWheels.Business.Interfaces
{
    public interface IRegisterOrderCommand
    {
        Guid CorrelationId { get; }
        string PickupName { get; }
        string PickupAddress { get; }
        string PickupCity { get; }
        string DeliveryName { get; }
        string DeliveryAddress { get; }
        string DeliveryCity { get; set; }
        

    }
 }

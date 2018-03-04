using FasTnT.Domain.Model.Users;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Events
{
    public class EpcisRequest
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime DocumentTime { get; set; }
        public virtual DateTime RecordTime { get; set; }
        public virtual string SubscriptionId { get; set; }
        public virtual IList<EpcisEvent> Events { get; set; } = new List<EpcisEvent>();

        public virtual void AddEvent(EpcisEvent @event)
        {
            @event.Request = this;
            Events.Add(@event);
        }
    }
}

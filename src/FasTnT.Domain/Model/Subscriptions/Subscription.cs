using FasTnT.Domain.Model.Users;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class Subscription
    {
        public Subscription()
        {
            Parameters = new List<SubscriptionParameter>();
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual SubscriptionSchedule Schedule { get; set; }
        public virtual string DestinationUrl { get; set; }
        public virtual string QueryName { get; set; }
        public virtual IList<SubscriptionParameter> Parameters { get; set; }
        public virtual SubscriptionControls Controls { get; set; }
        public virtual DateTime LastRunOn { get; set; }
        public virtual User User { get; set; }

        public virtual IList<SubscriptionPendingRequest> PendingRequests { get; set; }

        public virtual void AddParameter(SubscriptionParameter parameter)
        {
            parameter.Subscription = this;
            Parameters.Add(parameter);
        }
    }
}

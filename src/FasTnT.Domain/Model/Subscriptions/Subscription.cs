using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class Subscription
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual SubscriptionSchedule Schedule { get; set; }
        public virtual string DestinationUrl { get; set; }
        public virtual string DestinationHeaders { get; set; }
        public virtual string QueryName { get; set; }
        public virtual IList<SubscriptionParameter> Parameters { get; set; }
        public virtual SubscriptionControls Controls { get; set; }
        public virtual DateTime LastRunOn { get; set; }
    }
}

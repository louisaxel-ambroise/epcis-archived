using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class SubscriptionParameter
    {
        public virtual Guid Id { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual string ParameterName { get; set; }
        public virtual IList<SubscriptionParameterValue> Values { get; set; } = new List<SubscriptionParameterValue>();
    }
}
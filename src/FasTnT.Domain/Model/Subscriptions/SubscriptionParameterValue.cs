using System;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class SubscriptionParameterValue
    {
        public virtual Guid Id { get; set; }
        public virtual SubscriptionParameter Parameter { get; set; }
        public virtual string Value { get; set; }
    }
}
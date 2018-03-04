using System;
using System.Collections.Generic;
using FasTnT.Domain.Model.Queries;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class SubscriptionParameter
    {
        public SubscriptionParameter()
        {
            Values = new List<SubscriptionParameterValue>();
        }

        public virtual Guid Id { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual string ParameterName { get; set; }
        public virtual IList<SubscriptionParameterValue> Values { get; set; } = new List<SubscriptionParameterValue>();

        public static SubscriptionParameter Parse(QueryParam parameter)
        {
            var subscriptionParameter = new SubscriptionParameter
            {
                ParameterName = parameter.Name
            };

            foreach(var value in parameter.Values)
            {
                subscriptionParameter.Values.Add(new SubscriptionParameterValue
                {
                    Parameter = subscriptionParameter,
                    Value = value
                });
            }

            return subscriptionParameter;
        }
    }
}
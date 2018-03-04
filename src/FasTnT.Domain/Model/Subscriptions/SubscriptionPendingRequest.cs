using FasTnT.Domain.Model.Events;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class SubscriptionPendingRequest
    {
        public virtual Subscription Subscription { get; set; }
        public virtual EpcisRequest Request { get; set; }

        public override bool Equals(object obj)
        {
            var request = obj as SubscriptionPendingRequest;
            return request != null &&
                   EqualityComparer<Subscription>.Default.Equals(Subscription, request.Subscription) &&
                   EqualityComparer<EpcisRequest>.Default.Equals(Request, request.Request);
        }

        public override int GetHashCode()
        {
            var hashCode = 115383334;
            hashCode = hashCode * -1521134295 + EqualityComparer<Subscription>.Default.GetHashCode(Subscription);
            hashCode = hashCode * -1521134295 + EqualityComparer<EpcisRequest>.Default.GetHashCode(Request);
            return hashCode;
        }
    }
}

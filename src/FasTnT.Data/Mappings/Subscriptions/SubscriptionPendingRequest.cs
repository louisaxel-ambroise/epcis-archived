using FasTnT.Domain.Model.Subscriptions;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Subscriptions
{
    public class SubscriptionPendingRequestMap : ClassMap<SubscriptionPendingRequest>
    {
        public SubscriptionPendingRequestMap()
        {
            Table("pendingrequest");
            Schema("subscriptions");

            CompositeId()
                .KeyReference(x => x.Request, "request_id")
                .KeyReference(x => x.Subscription, "subscription_id");
        }
    }
}

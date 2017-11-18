using FasTnT.Domain.Model.Subscriptions;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Subscriptions
{
    public class SubscriptionParameterMap : ClassMap<SubscriptionParameter>
    {
        public SubscriptionParameterMap()
        {
            Schema("subscriptions");
            Table("parameter");

            Id(x => x.Id).Column("id");
            Map(x => x.ParameterName).Column("name").Not.Nullable();

            References(x => x.Subscription).Column("subscription_id");
        }
    }
}

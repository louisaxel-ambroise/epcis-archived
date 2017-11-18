using FasTnT.Domain.Model.Subscriptions;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Subscriptions
{
    public class SubscriptionParameterValueMap : ClassMap<SubscriptionParameterValue>
    {
        public SubscriptionParameterValueMap()
        {
            Schema("subscriptions");
            Table("parameter_value");

            Id(x => x.Id).Column("id");
            Map(x => x.Value).Column("value").Not.Nullable();

            References(x => x.Parameter).Column("parameter_id");
        }
    }
}

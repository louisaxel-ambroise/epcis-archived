using FasTnT.Domain.Model.Subscriptions;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Subscriptions
{
    public class SubscriptionMap : ClassMap<Subscription>
    {
        public SubscriptionMap()
        {
            Table("subscription");
            Schema("subscriptions");

            Id(x => x.Id).Column("id");
            Map(x => x.QueryName).Column("query_name");
            Map(x => x.DestinationUrl).Column("destination_url");
            Map(x => x.DestinationHeaders).Column("destination_headers");
            Component(x => x.Controls).ColumnPrefix("controls_");

            References(x => x.Schedule).Column("schedule_id");
            HasMany(x => x.Parameters).KeyColumn("subscription_id").Inverse();
            HasManyToMany(x => x.PendingRequests).Table("pendingrequest").Schema("subscriptions").ParentKeyColumn("subscription_id").ChildKeyColumn("request_id").Cascade.All();
        }
    }
}

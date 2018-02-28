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
            Map(x => x.Name).Column("subscription_name");
            Map(x => x.QueryName).Column("query_name");
            Map(x => x.DestinationUrl).Column("destination_url");
            Component(x => x.Controls).ColumnPrefix("controls_");

            References(x => x.User).Column("user_id").Nullable();
            References(x => x.Schedule).Column("schedule_id").Cascade.SaveUpdate();
            HasMany(x => x.Parameters).KeyColumn("subscription_id").Cascade.All();
            HasMany(x => x.PendingRequests).Table("pendingrequest").Schema("subscriptions").KeyColumn("subscription_id").LazyLoad().Cascade.None();
        }
    }
}

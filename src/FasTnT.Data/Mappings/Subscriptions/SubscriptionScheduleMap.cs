using FasTnT.Domain.Model.Subscriptions;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Subscriptions
{
    public class SubscriptionScheduleMap : ClassMap<SubscriptionSchedule>
    {
        public SubscriptionScheduleMap()
        {
            Table("schedule");
            Schema("subscriptions");

            Id(x => x.Id).Column("id");
            Map(x => x.Seconds).Column("seconds");
            Map(x => x.Minutes).Column("minutes");
            Map(x => x.Hours).Column("hours");
            Map(x => x.DayOfMonth).Column("day_of_month");
            Map(x => x.Month).Column("month");
            Map(x => x.DayOfWeek).Column("day_of_week");
        }
    }
}

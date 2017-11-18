using FasTnT.Domain.Model.Subscriptions;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Subscriptions
{
    public class SubscriptionControlsMap : ComponentMap<SubscriptionControls>
    {
        public SubscriptionControlsMap()
        {
            Map(x => x.InitialRecordTime).Column("initial_record_time");
            Map(x => x.ReportIfEmpty).Column("report_if_empty");
        }
    }
}

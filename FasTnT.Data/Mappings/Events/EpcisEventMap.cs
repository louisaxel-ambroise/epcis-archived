using FasTnT.Domain.Model.Events;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class EpcisEventMap : ClassMap<EpcisEvent>
    {
        // TODO: generate sequential GUIDs to improve DB performances.
        public EpcisEventMap()
        {
            Table("event");
            Schema("epcis");

            Id(x => x.Id).Column("id").GeneratedBy.Assigned();

            Map(x => x.Action).Column("action").CustomType<EventAction>();
            Map(x => x.EventType).Column("event_type").CustomType<EventType>();
            Map(x => x.CaptureTime).Column("capture_time");
            Map(x => x.EventTime).Column("record_time");
            Map(x => x.TransformationId).Column("transformation_id");
            Component(x => x.EventTimezoneOffset, m => m.Map(x => x.Value).Column("event_timezone_offset"));

            References(x => x.BusinessLocation).Column("business_location");
            References(x => x.BusinessStep).Column("business_step");
            References(x => x.Disposition).Column("disposition");
            References(x => x.ReadPoint).Column("read_point");

            HasMany(x => x.Epcs).KeyColumn("event_id");
            HasMany(x => x.CustomFields).KeyColumn("event_id");
            HasMany(x => x.BusinessTransactions).KeyColumn("event_id");
        }
    }
}

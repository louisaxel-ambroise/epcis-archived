using FasTnT.Domain.Model.Events;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class EpcisEventMap : ClassMap<EpcisEvent>
    {
        public EpcisEventMap()
        {
            Table("event");
            Schema("epcis");

            Id(x => x.Id).Column("id").GeneratedBy.GuidComb();

            Map(x => x.Action).Column("action").CustomType<EventAction>();
            Map(x => x.EventType).Column("event_type").CustomType<EventType>();
            Map(x => x.EventTime).Column("record_time");
            Map(x => x.TransformationId).Column("transformation_id");
            Component(x => x.EventTimezoneOffset, m => m.Map(x => x.Value).Column("event_timezone_offset"));

            Map(x => x.BusinessLocation).Column("business_location");
            Map(x => x.BusinessStep).Column("business_step");
            Map(x => x.Disposition).Column("disposition");
            Map(x => x.ReadPoint).Column("read_point");

            References(x => x.Request).Column("request_id").Not.Nullable();

            HasMany(x => x.Epcs).KeyColumn("event_id").Inverse().Cascade.Persist().NotFound.Ignore().LazyLoad();
            HasMany(x => x.CustomFields).KeyColumn("event_id").Inverse().Cascade.Persist().NotFound.Ignore().LazyLoad();
            HasMany(x => x.BusinessTransactions).KeyColumn("event_id").Inverse().Cascade.Persist().NotFound.Ignore().LazyLoad();
            HasMany(x => x.SourcesDestinations).KeyColumn("event_id").Inverse().Cascade.Persist().NotFound.Ignore().LazyLoad();
        }
    }
}

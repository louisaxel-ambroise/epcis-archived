using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;

namespace Epcis.Database.Mappings
{
    public class BaseEventMap : ClassMap<BaseEvent>
    {
        public BaseEventMap()
        {
            Schema(DatabaseConstants.Schemas.Epcis);
            Table(DatabaseConstants.Tables.Event);

            DiscriminateSubClassesOnColumn("Type");

            Id(x => x.Id).Column("Id").GeneratedBy.GuidComb();
            Map(x => x.EventTime).Column("EventTime");
            Map(x => x.RecordTime).Column("RecordTime");
            Map(x => x.Action).Column("Action").CustomType<EventAction>();
            References(x => x.BusinessLocation).Column("BizLocationId").Not.Nullable();
        }        
    }
}
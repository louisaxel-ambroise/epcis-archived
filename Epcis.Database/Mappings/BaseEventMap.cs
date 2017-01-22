using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;
using NHibernate.Type;

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
            Map(x => x.Extension).Column("Extension").CustomType<XDocType>();
            References(x => x.BusinessLocation).Column("BizLocationId").Nullable();
            References(x => x.BusinessStep).Column("BizStepId").Nullable();
            References(x => x.Disposition).Column("DispositionId").Nullable();
            References(x => x.ReadPoint).Column("ReadPointId").Nullable();

            HasManyToMany(x => x.Sources).Schema(DatabaseConstants.Schemas.Epcis).Table(DatabaseConstants.Tables.EventToSource).ParentKeyColumn("EventId").ChildKeyColumn("SourceId");
            HasManyToMany(x => x.Destinations).Schema(DatabaseConstants.Schemas.Epcis).Table(DatabaseConstants.Tables.EventToDestination).ParentKeyColumn("EventId").ChildKeyColumn("DestinationId");
        }        
    }
}
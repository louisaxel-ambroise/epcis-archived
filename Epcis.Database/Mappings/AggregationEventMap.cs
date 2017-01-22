using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;

namespace Epcis.Database.Mappings
{
    public class AggregationEventMap : SubclassMap<AggregationEvent>
    {
        public AggregationEventMap()
        {
            DiscriminatorValue("agg");

            Map(x => x.Action).Column("Action").CustomType<EventAction>().Not.Nullable();

            References(x => x.Parent).Column("ParentId").Cascade.None();
            HasManyToMany(x => x.ChildEpcs).Schema(DatabaseConstants.Schemas.Epcis).Table(DatabaseConstants.Tables.EventToEpc).ParentKeyColumn("EventId").ChildKeyColumn("EpcId");
        }
    }
}
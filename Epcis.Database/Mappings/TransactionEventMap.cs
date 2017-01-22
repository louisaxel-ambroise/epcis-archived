using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;

namespace Epcis.Database.Mappings
{
    public class TransactionEventMap : SubclassMap<TransactionEvent>
    {
        public TransactionEventMap()
        {
            DiscriminatorValue("tsn");

            Map(x => x.Action).Column("Action").CustomType<EventAction>().Not.Nullable();
            HasManyToMany(x => x.Epcs).Schema(DatabaseConstants.Schemas.Epcis).Table(DatabaseConstants.Tables.EventToEpc).ParentKeyColumn("EventId").ChildKeyColumn("EpcId");
        }
    }
}
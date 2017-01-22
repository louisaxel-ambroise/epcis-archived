using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;

namespace Epcis.Database.Mappings
{
    public class ObjectEventMap : SubclassMap<ObjectEvent>
    {
        public ObjectEventMap()
        {
            DiscriminatorValue("object");

            HasManyToMany(x => x.Epcs).Schema(DatabaseConstants.Schemas.Epcis).Table(DatabaseConstants.Tables.EventToEpc).ParentKeyColumn("EventId").ChildKeyColumn("EpcId");
        }
    }
}
using FasTnT.Domain.Model;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class SourceDestinationMap : ClassMap<SourceDestination>
    {
        public SourceDestinationMap()
        {
            Table("source_destination");
            Schema("epcis");

            CompositeId()
                .KeyReference(x => x.Event, "event_id")
                .KeyProperty(x => x.Type, "type")
                .KeyProperty(x => x.Id, "source_dest_id");

            Map(x => x.Direction).Column("direction").CustomType<SourceDestinationType>();
        }
    }
}

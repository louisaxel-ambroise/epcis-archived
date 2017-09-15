using FasTnT.Domain.Model.Events;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class EpcMap : ClassMap<Epc>
    {
        public EpcMap()
        {
            Table("epc");
            Schema("epcis");
            ReadOnly();

            CompositeId()
                .KeyReference(x => x.Event, "event_id")
                .KeyProperty(x => x.Id, "epc");

            Map(x => x.IsQuantity).Column("is_quantity").Not.Nullable();
            Map(x => x.Quantity).Column("quantity").Nullable();
            Map(x => x.UnitOfMeasure).Column("unit_of_measure").Nullable();
            Map(x => x.Type).Column("type").Not.Nullable().CustomType<EpcType>();
        }
    }
}

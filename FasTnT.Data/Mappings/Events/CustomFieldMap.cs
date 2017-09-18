using FasTnT.Domain.Model.Events;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class CustomFieldMap : ClassMap<CustomField>
    {
        public CustomFieldMap()
        {
            Table("custom_field");
            Schema("epcis");

            CompositeId()
                .KeyProperty(x => x.Namespace, "namespace")
                .KeyProperty(x => x.Name, "name")
                .KeyReference(x => x.Event, "event_id");

            Map(x => x.Value).Column("value");
            Map(x => x.Type).Column("type").CustomType<FieldType>();
        }
    }
}

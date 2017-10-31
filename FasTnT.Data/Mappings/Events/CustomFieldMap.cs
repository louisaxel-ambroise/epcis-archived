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
                .KeyReference(x => x.Event, "event_id")
                .KeyProperty(x => x.Id, "field_id");

            Map(x => x.Name).Column("name");
            Map(x => x.Namespace).Column("namespace");
            Map(x => x.Value).Column("value");
            Map(x => x.Type).Column("type").CustomType<FieldType>();

            HasMany(x => x.Children).KeyColumns.Add("event_id", "parent_id").Cascade.Persist();
        }
    }
}

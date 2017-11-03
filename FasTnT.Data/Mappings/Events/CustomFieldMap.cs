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
            Map(x => x.Type).Column("type").CustomType<FieldType>();
            Map(x => x.ParentId).Column("parent_id");

            Map(x => x.TextValue).Column("text_value");
            Map(x => x.NumericValue).Column("numeric_value");
            Map(x => x.DateValue).Column("date_value");

            HasMany(x => x.Children).KeyColumns.Add("event_id", "parent_id").Cascade.Persist();
        }
    }
}

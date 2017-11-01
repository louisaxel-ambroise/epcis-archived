using System.Collections.Generic;

namespace FasTnT.Domain.Model.Events
{
    public class CustomField
    {
        public virtual int Id { get; set; }
        public virtual FieldType Type { get; set; }
        public virtual string Name { get; set; }
        public virtual string Namespace { get; set; }
        public virtual string Value { get; set; }
        public virtual EpcisEvent Event { get; set; }
        public virtual int? ParentId { get; set; }
        public virtual IList<CustomField> Children { get; set; } = new List<CustomField>();

        public override bool Equals(object obj)
        {
            var other = obj as CustomField;

            if (other == null) return false;
            return Namespace.Equals(other.Namespace) && Name.Equals(other.Name) && Event.Equals(other.Event);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Event.GetHashCode();
        }
    }
}

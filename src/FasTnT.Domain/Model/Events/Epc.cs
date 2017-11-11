namespace FasTnT.Domain.Model.Events
{
    public class Epc
    {
        public virtual string Id { get; set; }
        public virtual EpcType Type { get; set; }
        public virtual bool IsQuantity { get; set; }
        public virtual string UnitOfMeasure { get; set; }
        public virtual float? Quantity { get; set; }
        public virtual EpcisEvent Event { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Epc;

            if (other == null) return false;
            return Id.Equals(other.Id) && Event.Equals(other.Event);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Event.GetHashCode();
        }
    }
}
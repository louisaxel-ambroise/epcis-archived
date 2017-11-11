using FasTnT.Domain.Model.Events;

namespace FasTnT.Domain.Model
{
    public class SourceDestination
    {
        public virtual EpcisEvent Event { get; set; }
        public virtual string Type { get; set; }
        public virtual string Id { get; set; }
        public virtual SourceDestinationType Direction { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as SourceDestination;
            if (other == null) return false;

            return Event.Equals(other.Event) && Type.Equals(other.Type) && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Event.GetHashCode() ^ Type.GetHashCode() ^ Id.GetHashCode();
        }
    }
}

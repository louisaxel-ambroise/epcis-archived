namespace FasTnT.Domain.Model.MasterData
{
    public class SourceDestination
    {
        public virtual string Type { get; set; }
        public virtual string Id { get; set; }
        public virtual SourceDestinationType Direction { get; set; }
    }
}

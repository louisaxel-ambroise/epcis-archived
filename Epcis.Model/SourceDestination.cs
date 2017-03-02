namespace Epcis.Model
{
    public class SourceDestination
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public SourceDestinationDirection Direction { get; set; }
    }
}
namespace FasTnT.Domain.Model.Queries
{
    public class Subscription
    {
        public string Name { get; set; }
        public string Schedule { get; set; }
        public string Destination { get; set; }
        public string QueryName { get; set; }
        public string Id { get; set; }
        public QueryParams Parameters { get; set; }
        public SubscriptionControls Controls { get; set; }
    }
}

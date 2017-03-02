using Epcis.Model.Queries;

namespace Epcis.Model.Subscriptions
{
    public class Subscription
    {
        public string Id { get; set; }
        public string QueryName { get; set; }
        public string Destination { get; set; }
        public SubscriptionControls Controls{ get; set; }
        public EpcisQuery Parameters { get; set; }
    }
}
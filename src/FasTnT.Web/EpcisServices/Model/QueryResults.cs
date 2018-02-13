using System.ServiceModel;

namespace FasTnT.Web.EpcisServices
{
    [MessageContract]
    public class QueryResults
    {
        [MessageBodyMember(Name = "queryName", Order = 0)]
        public string QueryName { get; set; }

        [MessageBodyMember(Name = "subscriptionID", Order = 1)]
        public string SubscriptionId { get; set; }

        // TODO: eventList parameter is missing
    }
}

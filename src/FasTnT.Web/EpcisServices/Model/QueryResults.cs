using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "queryResults", IsReference = false)]
    public class QueryResults
    {
        [DataMember(Name = "queryName", IsRequired = true)]
        public string QueryName { get; set; }

        [DataMember(Name = "subscriptionID", IsRequired = false)]
        public string SubscriptionId { get; set; }

        [DataMember(Name = "resultsBody")]
        public QueryResultBody ResultBody { get; set; }
    }
}

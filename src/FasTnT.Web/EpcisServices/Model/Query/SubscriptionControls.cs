using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "controls", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class SubscriptionControlsRequest
    {
        [DataMember(Name = "reportIfEmpty")]
        public bool ReportIfEmpty { get; set; }

        [DataMember(Name = "schedule")]
        public QuerySchedule Schedule { get; set; }
    }
}

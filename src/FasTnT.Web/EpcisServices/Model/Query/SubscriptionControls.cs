using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "SubscriptionControls", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class SubscriptionControls
    {
        [DataMember(Name = "reportIfEmpty")]
        public bool ReportIfEmpty { get; set; }

        [DataMember(Name = "schedule")]
        public QuerySchedule Schedule { get; set; }
    }
}

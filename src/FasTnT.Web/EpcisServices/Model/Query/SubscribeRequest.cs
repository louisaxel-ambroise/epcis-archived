using System.ServiceModel;

namespace FasTnT.Web.EpcisServices
{
    [MessageContract(WrapperName = "subscribe", WrapperNamespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class SubscribeRequest
    {
        [MessageBodyMember(Name = "queryName", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        public string QueryName { get; set; }

        [MessageBodyMember(Name = "parameters", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        public QueryParams Parameters { get; set; }

        [MessageBodyMember(Name = "controls", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        public SubscriptionControls Controls { get; set; }

        [MessageBodyMember(Name = "subscriptionId", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        public string SubscriptionId { get; set; }

        [MessageBodyMember(Name = "url", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        public string Destination { get; set; }
    }
}

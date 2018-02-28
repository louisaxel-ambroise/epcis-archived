using System;
using System.ServiceModel;

namespace FasTnT.Web.EpcisServices
{
    [MessageContract(WrapperName = "subscribe", WrapperNamespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class SubscribeRequest
    {
        [MessageBodyMember(Name = "queryName", Namespace = "http://schemas.xmlsoap.org/wsdl/", Order = 0)]
        public string QueryName { get; set; }

        [MessageBodyMember(Name = "parameters", Namespace = "http://schemas.xmlsoap.org/wsdl/", Order = 1)]
        public QueryParams Parameters { get; set; }

        [MessageBodyMember(Name = "controls", Namespace = "http://schemas.xmlsoap.org/wsdl/", Order = 2)]
        public SubscriptionControls Controls { get; set; }

        [MessageBodyMember(Name = "subscriptionId", Namespace = "http://schemas.xmlsoap.org/wsdl/", Order = 3)]
        public string SubscriptionId { get; set; }

        [MessageBodyMember(Name = "url", Namespace = "http://schemas.xmlsoap.org/wsdl/", Order = 4)]
        public Uri Destination { get; set; }
    }
}

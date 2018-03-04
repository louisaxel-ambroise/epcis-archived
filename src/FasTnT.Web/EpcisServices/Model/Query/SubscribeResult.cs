using System.ServiceModel;

namespace FasTnT.Web.EpcisServices
{
    [MessageContract(WrapperName = "subscribeResult", WrapperNamespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class SubscribeResult
    {
        [MessageBodyMember(Name = "subscriptionId", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        public string SubscriptionId { get; set; }
    }
}

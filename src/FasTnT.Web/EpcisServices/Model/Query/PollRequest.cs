using System.ServiceModel;

namespace FasTnT.Web.EpcisServices
{
    [MessageContract(WrapperName = "poll", WrapperNamespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class PollRequest
    {
        [MessageBodyMember(Name = "queryName", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        public string QueryName { get; set; }

        [MessageBodyMember(Name = "queryParams", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        public QueryParams parameters { get; set; }
    }
}

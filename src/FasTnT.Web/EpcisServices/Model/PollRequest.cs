using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "pollRequest", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class PollRequest
    {
        [DataMember(Name = "queryName", Order = 0)]
        public string QueryName { get; set; }
        [DataMember(Name = "params", Order = 1)]
        public QueryParams Params { get; set; }
    }
}

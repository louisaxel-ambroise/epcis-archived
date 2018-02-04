using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "queryParam", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class QueryParam
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}

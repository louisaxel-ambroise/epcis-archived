using System.Runtime.Serialization;
using System.ServiceModel;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "queryParam", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class QueryParameter
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "values")]
        public ParamValues Values { get; set; }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "queryParam", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class QueryParam
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "values")]
        public ParamValues Values { get; set; }
    }

    [CollectionDataContract(Name = "values", ItemName = "value", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class ParamValues : List<string>
    {
    }
}

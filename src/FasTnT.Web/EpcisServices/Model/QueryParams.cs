using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [CollectionDataContract(Name = "params", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class QueryParams : List<QueryParam>
    {
    }
}

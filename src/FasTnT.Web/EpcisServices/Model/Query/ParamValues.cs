using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [CollectionDataContract(Name = "values", ItemName = "value", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class ParamValues : List<string>
    { 
    }
}

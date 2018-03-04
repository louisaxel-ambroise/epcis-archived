using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [CollectionDataContract(Name = "getQueryNamesResult")]
    public class QueryNamesResult : List<string>
    {
    }
}
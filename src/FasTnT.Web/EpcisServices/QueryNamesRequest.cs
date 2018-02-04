using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "getQueryNamesRequest")]
    public class QueryNamesRequest : EmptyParms
    {
    }

    [CollectionDataContract(Name = "getQueryNamesResult")]
    public class QueryNamesResult : List<string>
    {
    }

    [DataContract(Name = "EmptyParms")]
    public class EmptyParms
    {
    }
}
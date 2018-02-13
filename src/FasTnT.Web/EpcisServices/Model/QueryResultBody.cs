using System.Runtime.Serialization;
using System.Xml.Linq;
using System;
using System.IO;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "resultsBody")]
    public class QueryResultBody
    {
        [DataMember(Name = "EventList", EmitDefaultValue = false)]
        public XElement[] EventList { get; set; }

        public void InitializeFrom(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}

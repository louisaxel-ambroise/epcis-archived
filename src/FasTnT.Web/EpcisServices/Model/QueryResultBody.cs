using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "resultsBody")]
    public class QueryResultBody
    {
        [DataMember(Name = "EventList")]
        public EventListType EventList { get; set; }
    }
}

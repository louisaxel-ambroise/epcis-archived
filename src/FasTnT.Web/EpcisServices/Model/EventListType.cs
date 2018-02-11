using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [CollectionDataContract(Name = "EventList")]
    public class EventListType : List<Event>
    {
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Linq;

namespace FasTnT.Web.EpcisServices
{
    [CollectionDataContract(Name = "eventList", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class EventListType : List<Event>
    {
        public EventListType()
        {
        }

        public EventListType(IEnumerable<Event> collection) : base(collection)
        {
        }
    }

    [MessageContract(IsWrapped = false)]
    public class Event : XElement
    {
        public Event(XName name) : base(name) { }
        public Event(XElement other) : base(other) { }
        public Event(XStreamingElement other) : base(other) { }
        public Event(XName name, object content) : base(name, content) { }
        public Event(XName name, params object[] content) : base(name, content) { }
    }
}

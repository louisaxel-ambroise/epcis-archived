using System;
using System.ServiceModel;

namespace FasTnT.Web.EpcisServices.Model
{
    [MessageContract(IsWrapped = true, WrapperName = "CaptureEventsResponse", WrapperNamespace = "")]
    public class CaptureEventsResponse
    {
        [MessageBodyMember(Name = "CaptureStart", Order = 1, Namespace = "")]
        public DateTime CaptureStart { get; set; }
        [MessageBodyMember(Name = "CaptureEnd", Order = 2, Namespace = "")]
        public DateTime CaptureEnd { get; set; }
        [MessageBodyMember(Name = "EventsCount", Order = 3, Namespace = "")]
        public int EventsCount { get; set; }
        [MessageBodyMember(Name = "EventIds", Order = 4, Namespace = "")]
        public string[] EventIds { get; set; }
    }
}
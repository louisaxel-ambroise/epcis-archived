using System;

namespace FasTnT.Web.Models.Events
{
    public class EventDetailViewModel
    {
        public Guid Id { get; set; }
        public DateTime CaptureTime { get; set; }
        public DateTime EventTime { get; set; }
        public string EventTimeZoneOffset { get; set; }
        public string EventType { get; set; }
        public string Action { get; set; }
        public string BizLocation { get; set; }
        public string BizStep { get; set; }
    }
}
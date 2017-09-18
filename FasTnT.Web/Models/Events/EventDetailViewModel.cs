using System;

namespace FasTnT.Web.Models.Events
{
    public class EventDetailViewModel
    {
        public Guid Id { get; set; }
        public DateTime CapturedOn { get; set; }
        public DateTime EventTime { get; set; }
        public string EventTimeZoneOffset { get; set; }
    }
}
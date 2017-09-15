using System;

namespace FasTnT.Web.Models.Events
{
    public class EventSummaryViewModel
    {
        public Guid Id { get; set; }
        public DateTime CaptureTime { get; set; }
        public DateTime RecordTime { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string Location { get; set; }
        public string ReadPoint { get; set; }
    }
}
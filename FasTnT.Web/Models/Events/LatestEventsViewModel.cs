using System.Collections.Generic;

namespace FasTnT.Web.Models.Events
{
    public class LatestEventsViewModel
    {
        public IEnumerable<EventSummaryViewModel> Events { get; set; }
        public long Total { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Capture
{
    public class CaptureResponse
    {
        public int EventCount { get; set; }
        public DateTime CaptureStartDateUtc { get; set; }
        public DateTime CaptureEndDateUtc { get; set; }
        public IEnumerable<string> EventIds { get; set; }
    }
}

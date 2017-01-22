using System.Collections.Generic;

namespace Epcis.Domain.Services.Mapping
{
    public class EventParameters
    {
        public EventParameters()
        {
            Extensions = new Dictionary<string, object>();
            Ilmd = new Dictionary<string, object>();
        }

        // Common fields
        public string Type { get; set; }
        public string EventTime { get; set; }
        public string EventTimezoneOffset { get; set; }
        public string Action { get; set; } 
        public string BusinessStep { get; set; }
        public string BusinessLocation { get; set; }
        public string ReadPoint { get; set; }
        public string Disposition { get; set; }
        public IDictionary<string, object> Extensions { get; set; }

        // OBJECT events fields
        public string[] Epcs { get; set; }
        public IDictionary<string, object> Ilmd { get; set; }

        // TRANSFORMATION events fields
        public string ParentId { get; set; }
        public string[] ChildEpcs { get; set; }
        public ChildQuantity[] ChildQuantityList { get; set; }
    }
}
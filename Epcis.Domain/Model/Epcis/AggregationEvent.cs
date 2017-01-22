
using System.Collections.Generic;

namespace Epcis.Domain.Model.Epcis
{
    public class AggregationEvent : BaseEvent
    {
        public AggregationEvent()
        {
            ChildEpcs = new List<Epc>();
        }

        public virtual EventAction Action { get; set; }
        public virtual IList<Epc> ChildEpcs { get; set; }
        public virtual Epc Parent { get; set; }
    }
}
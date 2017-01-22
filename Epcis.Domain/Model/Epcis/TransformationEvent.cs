using System.Collections.Generic;

namespace Epcis.Domain.Model.Epcis
{
    public class TransformationEvent : BaseEvent
    {
        public TransformationEvent()
        {
            OutputEpcs = new List<Epc>();
            InputEpcs = new List<Epc>();
        }

        public virtual IList<Epc> InputEpcs { get; set; }
        public virtual IList<Epc> OutputEpcs { get; set; }
    }
}
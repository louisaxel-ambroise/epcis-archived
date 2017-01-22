using System.Collections.Generic;

namespace Epcis.Domain.Model.Epcis
{
    public class ObjectEvent : BaseEvent
    {
        public ObjectEvent()
        {
            Epcs = new List<Epc>();
        }

        public virtual IList<Epc> Epcs { get; set; }
        public virtual object Ilmd { get; set; } // TODO: store ILMD as key-value?
    }
}
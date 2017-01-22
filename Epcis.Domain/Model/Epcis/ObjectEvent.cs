using System.Collections.Generic;
using System.Xml.Linq;

namespace Epcis.Domain.Model.Epcis
{
    public class ObjectEvent : BaseEvent
    {
        public ObjectEvent()
        {
            Epcs = new List<Epc>();
        }

        public virtual EventAction Action { get; set; }
        public virtual IList<Epc> Epcs { get; set; }
        public virtual XDocument Ilmd { get; set; } // TODO: store ILMD as key-value?
    }
}
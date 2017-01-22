using System.Collections.Generic;

namespace Epcis.Domain.Model.Epcis
{
    public class TransactionEvent : BaseEvent
    {
        public TransactionEvent()
        {
            Epcs = new List<Epc>();
        }

        public virtual EventAction Action { get; set; }
        public virtual IList<Epc> Epcs { get; set; }
    }
}
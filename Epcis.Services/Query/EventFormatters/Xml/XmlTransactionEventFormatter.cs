using System.Xml.Linq;
using Epcis.Model.Events;

namespace Epcis.Services.Query.EventFormatters.Xml
{
    public class XmlTransactionEventFormatter : IEventFormatter<XElement>
    {
        public bool CanFormat(EpcisEvent epcisEvent)
        {
            return epcisEvent.EventType == EventType.Transaction;
        }

        public XElement Format(EpcisEvent epcisEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
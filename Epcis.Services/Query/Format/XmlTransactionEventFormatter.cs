using System.Xml.Linq;
using Epcis.Model;

namespace Epcis.Services.Query.Format
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
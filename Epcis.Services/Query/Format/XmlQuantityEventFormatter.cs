using System.Xml.Linq;
using Epcis.Model;

namespace Epcis.Services.Query.Format
{
    public class XmlQuantityEventFormatter : IEventFormatter<XElement>
    {
        public bool CanFormat(EpcisEvent epcisEvent)
        {
            return epcisEvent.EventType == EventType.Quantity;
        }

        public XElement Format(EpcisEvent epcisEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
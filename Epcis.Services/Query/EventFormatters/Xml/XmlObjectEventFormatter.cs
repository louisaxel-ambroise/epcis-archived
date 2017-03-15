using System.Linq;
using System.Xml.Linq;
using Epcis.Model.Events;

namespace Epcis.Services.Query.EventFormatters.Xml
{
    public class XmlObjectEventFormatter : IEventFormatter<XElement>
    {
        public static string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

        public bool CanFormat(EpcisEvent epcisEvent)
        {
            return epcisEvent.EventType == EventType.Object;
        }

        public XElement Format(EpcisEvent epcisEvent)
        {
            var element = new XElement("ObjectEvent");

            element.Add(new XElement("eventTime", epcisEvent.EventTime.ToString(DateTimeFormat)));
            element.Add(new XElement("recordTime", epcisEvent.CaptureTime.ToString(DateTimeFormat)));
            element.Add(new XElement("eventTimeZoneOffset", epcisEvent.EventTimezoneOffset.Representation));

            AddEpcList(epcisEvent, element);

            element.Add(new XElement("action", epcisEvent.Action.ToString().ToUpper()));

            if (!string.IsNullOrEmpty(epcisEvent.BusinessStep))
                element.Add(new XElement("bizStep", epcisEvent.BusinessStep));
            if (!string.IsNullOrEmpty(epcisEvent.Disposition))
                element.Add(new XElement("disposition", epcisEvent.Disposition));

            AddReadPoint(epcisEvent, element);
            AddBusinessLocation(epcisEvent, element);
            AddBusinessTransactions(epcisEvent, element);
            AddCustomFields(epcisEvent, element);

            return element;
        }

        private void AddBusinessTransactions(EpcisEvent epcisEvent, XElement element)
        {
            if (epcisEvent.BusinessTransactions == null || !epcisEvent.BusinessTransactions.Any()) return;

            var transactions = new XElement("bizTransactionList");

            foreach (var trans in epcisEvent.BusinessTransactions)
            {
                transactions.Add(new XElement("bizTransaction", trans.Id, new XAttribute("type", trans.Type)));
            }

            element.Add(transactions);
        }

        private static void AddCustomFields(EpcisEvent epcisEvent, XContainer element)
        {
            if (epcisEvent.CustomFields == null) return;

            foreach (var field in epcisEvent.CustomFields) element.Add(new XElement(XName.Get(field.Namespace, field.Name), field.Value));
        }

        private static void AddEpcList(EpcisEvent epcisEvent, XContainer element)
        {
            var epcList = new XElement("epcList");
            var epcQuantity = new XElement("epcQuantity");
            foreach (var epc in epcisEvent.Epcs.Where(x => x.Type == EpcType.List)) epcList.Add(new XElement("epc", epc.Id));
            foreach (var epc in epcisEvent.Epcs.Where(x => x.Type == EpcType.Quantity))
            {
                var qtyElement = new XElement("quantityElement");
                qtyElement.Add(new XElement("epcClass", epc.Id));
                if(epc.Quantity != null) qtyElement.Add(new XElement("quantity", epc.Quantity));
                if(!string.IsNullOrEmpty(epc.UnitOfMeasure)) qtyElement.Add(new XElement("uom", epc.UnitOfMeasure));

                epcQuantity.Add(qtyElement);
            }

            if (epcList.HasElements) element.Add(epcList);
            if (epcQuantity.HasElements) AddInExtension(element, epcQuantity);
        }

        private static void AddReadPoint(EpcisEvent epcisEvent, XContainer element)
        {
            if (epcisEvent.ReadPoint == null) return;

            var readPoint = new XElement("readPoint", new XElement("id", epcisEvent.ReadPoint.Id));

            element.Add(readPoint);
        }

        private static void AddBusinessLocation(EpcisEvent epcisEvent, XContainer element)
        {
            if (epcisEvent.BusinessLocation == null) return;
            
            element.Add(new XElement("bizLocation", new XElement("id", epcisEvent.BusinessLocation.Id)));
        }

        private static void AddInExtension(XContainer container, XElement element)
        {
            var extension = container.Element("extension");
            if (extension == null)
            {
                extension = new XElement("extension");
                container.Add(extension);
            }

            extension.Add(element);
        }
    }
}
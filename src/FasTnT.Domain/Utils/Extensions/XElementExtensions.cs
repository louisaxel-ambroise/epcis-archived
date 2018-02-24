using System.Collections.Generic;
using System.Xml.Linq;
using System;
using FasTnT.Domain.Model.Events;
using System.Linq;
using FasTnT.Domain.Services.Capture.Events;
using FasTnT.Domain.Model;

namespace FasTnT.Domain.Extensions
{
    public static class XElementExtensions
    {
        public static EventType ToEventType(this XElement element)
        {
            return (EventType)Enum.Parse(typeof(EventType), element.Name.LocalName, true);
        }

        public static EventAction ToEventAction(this XElement element)
        {
            return (EventAction)Enum.Parse(typeof(EventAction), element.Value, true);
        }

        public static void ParseEpcListInto(this XElement element, EpcisEvent destination)
        {
            foreach (var epc in element.Elements()) destination.Epcs.Add(new Epc { Event = destination, Type = EpcType.List, Id = epc.Value });
        }

        public static void ParseEpcListInto(this XElement element, EpcisEvent destination, bool isInput)
        {
            var type = isInput ? EpcType.InputEpc : EpcType.OutputEpc;

            foreach (var epc in element.Elements("epc")) destination.Epcs.Add(new Epc { Event = destination, Type = type, Id = epc.Value });
        }

        public static void ParseQuantityListInto(this XElement element, EpcisEvent destination, bool isInput)
        {
            foreach (var epc in element.Elements("quantityElement"))
            {
                destination.Epcs.Add(new Epc
                {
                    Event = destination,
                    Type = isInput ? EpcType.InputQuantity : EpcType.OutputQuantity,
                    Id = epc.Element("epcClass").Value,
                    IsQuantity = true,
                    Quantity = float.Parse(element.Element("Quantity").Value),
                    UnitOfMeasure = element.Element("uom") != null ? element.Element("uom").Value : null
                });
            }
        }

        public static void ParseChildEpcListInto(this XElement element, EpcisEvent destination)
        {
            foreach (var epc in element.Elements()) destination.Epcs.Add(new Epc { Event = destination, Type = EpcType.ChildEpc, Id = epc.Value });
        }

        public static IList<BusinessTransaction> ToBusinessTransactions(this XElement element)
        {
            return element.Elements("bizTransaction").Select(child => new BusinessTransaction { Type = child.Attribute("type").Value, Id = child.Value }).ToList();
        }

        public static void ParseReadPoint(this XElement element, EpcisEvent epcisEvent)
        {
            epcisEvent.ReadPoint = element.Element("id").Value;

            foreach (var innerElement in element.Elements().Where(x => x.Name.Namespace != XNamespace.None))
            {
                epcisEvent.CustomFields.Add(DocumentParser.ParseCustomField(innerElement, epcisEvent, FieldType.ReadPointExtension));
            }
        }

        public static void ParseSourceInto(this XElement element, IList<SourceDestination> list)
        {
            foreach (var child in element.Elements("source"))
            {
                list.Add(new SourceDestination
                {
                    Type = child.Attribute("type").Value,
                    Id = child.Value,
                    Direction = SourceDestinationType.Source
                });
            }
        }

        public static void ParseDestinationInto(this XElement element, IList<SourceDestination> list)
        {
            foreach (var child in element.Elements("destination"))
            {
                list.Add(new SourceDestination
                {
                    Type = child.Attribute("type").Value,
                    Id = child.Value,
                    Direction = SourceDestinationType.Destination
                });
            }
        }

        public static void ParseBusinessLocation(this XElement element, EpcisEvent epcisEvent)
        {
            foreach (var innerElement in element.Elements().Where(x => !new[] { "id", "corrective" }.Contains(x.Name.LocalName)))
            {
                epcisEvent.CustomFields.Add(DocumentParser.ParseCustomField(innerElement, epcisEvent, FieldType.BusinessLocationExtension));
            }

            epcisEvent.BusinessLocation = element.Element("id").Value;
        }

        public static ErrorDeclaration ToErrorDeclaration(this XElement element, EpcisEvent epcisEvent)
        {
            foreach (var innerElement in element.Elements().Where(x => !new[] { "id", "corrective" }.Contains(x.Name.LocalName)))
            {
                epcisEvent.CustomFields.Add(DocumentParser.ParseCustomField(innerElement, epcisEvent, FieldType.ErrorDeclarationExtension));
            }

            var declarationTime = DateTime.Parse(element.Element("declarationTime").Value);
            return new ErrorDeclaration { DeclarationTime = declarationTime, Reason = element.Element("reason").Value };
        }
    }
}

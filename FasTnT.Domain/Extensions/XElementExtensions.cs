using System.Collections.Generic;
using System.Xml.Linq;
using System;
using FasTnT.Domain.Model.Events;
using System.Linq;
using FasTnT.Domain.Model.MasterData;

namespace FasTnT.Domain.Extensions
{
    public static class XElementExtensions
    {
        public static EventType ToEventType(this XElement element)
        {
            var elementName = element.Name.LocalName.Remove(element.Name.LocalName.Length - 5);

            return (EventType)Enum.Parse(typeof(EventType), elementName, true);
        }

        public static EventAction ToEventAction(this XElement element)
        {
            return (EventAction)Enum.Parse(typeof(EventAction), element.Value, true);
        }

        public static void ParseEpcListInto(this XElement element, IList<Epc> destination)
        {
            foreach (var epc in element.Elements()) destination.Add(new Epc { Type = EpcType.List, Id = epc.Value });
        }

        public static void ParseEpcListInto(this XElement element, IList<Epc> destination, bool isInput)
        {
            var type = isInput ? EpcType.InputEpc : EpcType.OutputEpc;

            foreach (var epc in element.Elements("epc")) destination.Add(new Epc { Type = type, Id = epc.Value });
        }

        public static void ParseQuantityListInto(this XElement element, IList<Epc> destination, bool isInput)
        {
            foreach (var epc in element.Elements("quantityElement"))
            {
                destination.Add(new Epc
                {
                    Type = isInput ? EpcType.InputQuantity : EpcType.OutputQuantity,
                    Id = epc.Element("epcClass").Value,
                    IsQuantity = true,
                    Quantity = float.Parse(element.Element("Quantity").Value),
                    UnitOfMeasure = element.Element("uom") != null ? element.Element("uom").Value : null
                });
            }
        }

        public static void ParseChildEpcListInto(this XElement element, IList<Epc> destination)
        {
            foreach (var epc in element.Elements()) destination.Add(new Epc { Type = EpcType.ChildEpc, Id = epc.Value });
        }

        public static IList<BusinessTransaction> ToBusinessTransactions(this XElement element)
        {
            return element.Elements("bizTransaction").Select(child => new BusinessTransaction { Type = child.Attribute("type").Value, Id = child.Value }).ToList();
        }

        public static void ParseReadPoint(this XElement element, EpcisEvent epcisEvent)
        {
            epcisEvent.ReadPoint = new ReadPoint { Id = element.Element("id").Value };

            foreach (var innerElement in element.Elements().Where(x => x.Name.Namespace != XNamespace.None))
            {
                epcisEvent.CustomFields.Add(new CustomField
                {
                    Type = FieldType.ReadPointExtension,
                    Namespace = innerElement.Name.NamespaceName,
                    Name = innerElement.Name.LocalName,
                    Value = innerElement.Value
                });
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

        public static void ParseIlmd(this XElement element, EpcisEvent epcisEvent)
        {
            foreach (var inner in element.Elements())
            {
                epcisEvent.CustomFields.Add(new CustomField { Type = FieldType.Ilmd, Namespace = inner.Name.NamespaceName, Name = inner.Name.LocalName, Value = inner.Value });
            }
        }

        public static void ParseBusinessLocation(this XElement element, EpcisEvent epcisEvent)
        {
            foreach (var innerElement in element.Elements().Where(x => !new[] { "id", "corrective" }.Contains(x.Name.LocalName)))
            {
                epcisEvent.CustomFields.Add(new CustomField
                {
                    Type = FieldType.BusinessLocationExtension,
                    Namespace = innerElement.Name.NamespaceName,
                    Name = innerElement.Name.LocalName,
                    Value = innerElement.Value
                });
            }

            epcisEvent.BusinessLocation = new BusinessLocation { Id = element.Element("id").Value };
        }

        public static ErrorDeclaration ToErrorDeclaration(this XElement element, EpcisEvent epcisEvent)
        {
            foreach (var innerElement in element.Elements().Where(x => !new[] { "id", "corrective" }.Contains(x.Name.LocalName)))
            {
                epcisEvent.CustomFields.Add(new CustomField
                {
                    Type = FieldType.ErrorDeclarationExtension,
                    Namespace = innerElement.Name.NamespaceName,
                    Name = innerElement.Name.LocalName,
                    Value = innerElement.Value
                });
            }

            var declarationTime = DateTime.Parse(element.Element("declarationTime").Value);
            return new ErrorDeclaration { DeclarationTime = declarationTime, Reason = element.Element("reason").Value };
        }
    }
}

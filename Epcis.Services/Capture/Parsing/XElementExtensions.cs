using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using Epcis.Model.Events;

namespace Epcis.Services.Capture.Parsing
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public static class XElementExtensions
    {
        public static EventType ToEventType(this XElement element)
        {
            var elementName = element.Name.LocalName.Remove(element.Name.LocalName.Length-5);

            return (EventType)Enum.Parse(typeof(EventType), elementName, true);
        }

        public static EventAction ToEventAction(this XElement element)
        {
            return (EventAction) Enum.Parse(typeof (EventAction), element.Value, true);
        }

        public static void ParseEpcListInto(this XElement element, IList<Epc> destination)
        {
            foreach (var epc in element.Elements()) destination.Add(new Epc {Type = EpcType.List, Id = epc.Value});
        }

        public static void ParseEpcListInto(this XElement element, IList<Epc> destination, bool isInput)
        {
            var type = isInput ? EpcType.InputEpc : EpcType.OutputEpc;

            foreach (var epc in element.Elements("epc")) destination.Add(new Epc { Type = type, Id = epc.Value });
        }

        public static void ParseChildEpcListInto(this XElement element, IList<Epc> destination)
        {
            foreach (var epc in element.Elements()) destination.Add(new Epc { Type = EpcType.ChildEpc, Id = epc.Value });
        }

        public static IList<BusinessTransaction> ToBusinessTransactions(this XElement element)
        {
            return element.Elements("bizTransaction").Select(child => new BusinessTransaction { Type = child.Attribute("type").Value, Id = child.Value }).ToList();
        }

        public static BusinessLocation ToBusinessLocation(this XElement element)
        {
            return new BusinessLocation { Id = element.Element("id").Value };
        }

        public static ReadPoint ToReadPoint(this XElement element)
        {
            return new ReadPoint { Id = element.Element("id").Value };
        }

        public static ErrorDeclaration ToErrorDeclaration(this XElement element)
        {
            return new ErrorDeclaration{ DeclarationTime = DateTime.Parse(element.Element("declarationTime").Value), Reason = element.Element("reason").Value };
        }

        public static CustomField ToCustomField(this XElement element)
        {
            return new CustomField { Namespace = element.Name.NamespaceName, Name = element.Name.LocalName, Value = element.Value };
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
    }
}
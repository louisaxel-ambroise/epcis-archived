using System.Collections.Generic;
using System.Xml.Linq;
using System;
using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Exceptions;
using System.Linq;
using FasTnT.Domain.Extensions;

namespace FasTnT.Domain.Services.EventCapture
{
    public class DocumentParser : IDocumentParser
    {
        const string EpcisNamespace = "urn:epcglobal:epcis:xsd:1";
        static int NextCustomId = 1;
        
        // TODO: parse Query Response
        public virtual IEnumerable<EpcisEvent> Parse(XElement input)
        {
            if(input.Name.Equals(XName.Get("EPCISDocument", EpcisNamespace)))
            {
                return ParseEvents(input.Element("EPCISBody").Element("EventList"));
            }

            throw new EpcisException($"Unexpected XML element : '{input.Name.LocalName}'");
        }

        private static IEnumerable<EpcisEvent> ParseEvents(XContainer input)
        {
            var events = new List<EpcisEvent>();

            foreach (var element in input.Elements())
            {
                var name = element.Name.LocalName;

                if (name == "extension") events.AddRange(ParseEvents(element));
                else events.Add(ParseEvent(element));
            }

            return events;
        }

        private static EpcisEvent ParseEvent(XElement element)
        {
            NextCustomId = 1;
            var epcisEvent = new EpcisEvent { EventType = element.ToEventType() };
            ParseAttributes(element, epcisEvent);

            return epcisEvent;
        }

        private static void ParseAttributes(XContainer element, EpcisEvent epcisEvent)
        {
            foreach (var innerElement in element.Elements())
            {
                switch (innerElement.Name.LocalName)
                {
                    case "action":
                        epcisEvent.Action = innerElement.ToEventAction(); break;
                    case "eventTimeZoneOffset":
                        epcisEvent.EventTimezoneOffset = new TimeZoneOffset { Representation = innerElement.Value }; break;
                    case "eventTime":
                        epcisEvent.EventTime = DateTime.Parse(innerElement.Value); break;
                    case "epcList":
                        innerElement.ParseEpcListInto(epcisEvent); break;
                    case "childEPCs":
                        innerElement.ParseChildEpcListInto(epcisEvent); break;
                    case "inputQuantityList":
                        innerElement.ParseQuantityListInto(epcisEvent, true); break;
                    case "inputEpcList":
                        innerElement.ParseEpcListInto(epcisEvent, true); break;
                    case "outputQuantityList":
                        innerElement.ParseQuantityListInto(epcisEvent, false); break;
                    case "outputEpcList":
                        innerElement.ParseEpcListInto(epcisEvent, false); break;
                    case "epcClass":
                        epcisEvent.Epcs.Add(new Epc { Event = epcisEvent, Type = EpcType.Quantity, Id = innerElement.Value, IsQuantity = true }); break;
                    case "quantity":
                        epcisEvent.Epcs.Single(x => x.Type == EpcType.Quantity).Quantity = float.Parse(innerElement.Value); break;
                    case "bizStep":
                        epcisEvent.BusinessStep = innerElement.Value; break;
                    case "disposition":
                        epcisEvent.Disposition = innerElement.Value; break;
                    case "eventID":
                        epcisEvent.EventId = innerElement.Value; break;
                    case "errorDeclaration":
                        epcisEvent.ErrorDeclaration = innerElement.ToErrorDeclaration(epcisEvent); break;
                    case "transformationId":
                        epcisEvent.TransformationId = innerElement.Value; break;
                    case "bizLocation":
                        innerElement.ParseBusinessLocation(epcisEvent); break;
                    case "bizTransactionList":
                        epcisEvent.BusinessTransactions = innerElement.ToBusinessTransactions(); break;
                    case "readPoint":
                        innerElement.ParseReadPoint(epcisEvent); break;
                    case "sourceList":
                        innerElement.ParseSourceInto(epcisEvent.SourcesDestinations); break;
                    case "destinationList":
                        innerElement.ParseDestinationInto(epcisEvent.SourcesDestinations); break;
                    case "ilmd":
                        ParseIlmd(innerElement, epcisEvent); break;
                    case "parentID":
                        epcisEvent.Epcs.Add(new Epc { Event = epcisEvent, Id = innerElement.Value, Type = EpcType.ParentId }); break;
                    case "recordTime": // We don't process record time as it will be overrided in any case..
                        break;
                    case "extension":
                        ParseExtensionElement(innerElement, epcisEvent); break;
                    default:
                        epcisEvent.CustomFields.Add(ParseCustomField(innerElement, epcisEvent)); break;
                }
            }
        }

        private static void ParseExtensionElement(XElement innerElement, EpcisEvent epcisEvent)
        {
            if (innerElement.Name.Namespace == XNamespace.None || innerElement.Name.Namespace == XNamespace.Xmlns || innerElement.Name.NamespaceName == EpcisNamespace)
                ParseAttributes(innerElement, epcisEvent);
            else
                epcisEvent.CustomFields.Add(ParseCustomField(innerElement, epcisEvent));
        }

        private static void ParseIlmd(XElement element, EpcisEvent epcisEvent)
        {
            foreach(var children in element.Elements())
            {
                epcisEvent.CustomFields.Add(ParseCustomField(children, epcisEvent, FieldType.Ilmd));
            }
        }

        internal static CustomField ParseCustomField(XElement element, EpcisEvent epcisEvent, FieldType type = FieldType.EventExtension)
        {
            if (element.Name.Namespace == XNamespace.None || element.Name.Namespace == XNamespace.Xmlns || element.Name.NamespaceName == EpcisNamespace)
            {
                throw new EpcisException($"Element '{element.Name.LocalName}' with namespace '{element.Name.NamespaceName}' not expected here.");
            }
            
            var field = new CustomField
            {
                Id = NextCustomId++,
                Event = epcisEvent,
                Type = type,
                Name = element.Name.LocalName,
                Namespace = element.Name.NamespaceName,
                TextValue = element.HasElements ? null : element.Value,
                NumericValue = element.HasElements ? null : double.TryParse(element.Value, out double doubleValue) ? doubleValue : default(double?),
                DateValue = element.HasElements ? null : DateTime.TryParse(element.Value, out DateTime dateValue) ? dateValue : default(DateTime?),
            };

            if (element.HasElements)
            { 
                foreach(var children in element.Elements()) field.Children.Add(ParseCustomField(children, epcisEvent, type));
            }

            foreach(var attribute in element.Attributes().Where(x => x.Name.Namespace != XNamespace.None && x.Name.Namespace != XNamespace.Xmlns && x.Name.NamespaceName != EpcisNamespace))
            {
                var attributeField = new CustomField
                {
                    Id = NextCustomId++,
                    ParentId = field.Id,
                    Event = epcisEvent,
                    Type = FieldType.Attribute,
                    Name = attribute.Name.LocalName,
                    Namespace = attribute.Name.Namespace.NamespaceName,
                    TextValue = attribute.Value,
                    NumericValue = element.HasElements ? null : double.TryParse(element.Value, out double doubleVal) ? doubleVal : default(double?),
                    DateValue = element.HasElements ? null : DateTime.TryParse(element.Value, out DateTime dateVal) ? dateVal : default(DateTime?),
                };

                field.Children.Add(attributeField);
            }

            return field;
        }
    }
}

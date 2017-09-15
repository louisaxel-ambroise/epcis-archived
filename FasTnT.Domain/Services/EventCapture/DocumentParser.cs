using System.Collections.Generic;
using System.Xml.Linq;
using System;
using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Exceptions;
using System.Linq;
using FasTnT.Domain.Model.MasterData;
using FasTnT.Domain.Extensions;

namespace FasTnT.Domain.Services.EventCapture
{
    public class DocumentParser : IDocumentParser
    {
        const string EpcisNamespace = "urn:epcglobal:epcis:xsd:1";

        //[LogMethodCall]
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
                    case "extension":
                        ParseAttributes(innerElement, epcisEvent); break;
                    case "action":
                        epcisEvent.Action = innerElement.ToEventAction(); break;
                    case "eventTimeZoneOffset":
                        epcisEvent.EventTimezoneOffset = new TimeZoneOffset { Representation = innerElement.Value }; break;
                    case "eventTime":
                        epcisEvent.EventTime = DateTime.Parse(innerElement.Value); break;
                    case "epcList":
                        innerElement.ParseEpcListInto(epcisEvent.Epcs); break;
                    case "childEPCs":
                        innerElement.ParseChildEpcListInto(epcisEvent.Epcs); break;
                    case "inputQuantityList":
                        innerElement.ParseQuantityListInto(epcisEvent.Epcs, true); break;
                    case "inputEpcList":
                        innerElement.ParseEpcListInto(epcisEvent.Epcs, true); break;
                    case "outputQuantityList":
                        innerElement.ParseQuantityListInto(epcisEvent.Epcs, false); break;
                    case "outputEpcList":
                        innerElement.ParseEpcListInto(epcisEvent.Epcs, false); break;
                    case "epcClass":
                        epcisEvent.Epcs.Add(new Epc { Type = EpcType.Quantity, Id = innerElement.Value, IsQuantity = true }); break;
                    case "quantity":
                        epcisEvent.Epcs.Single(x => x.Type == EpcType.Quantity).Quantity = float.Parse(innerElement.Value); break;
                    case "bizStep":
                        epcisEvent.BusinessStep = new BusinessStep { Id = innerElement.Value }; break;
                    case "disposition":
                        epcisEvent.Disposition = new Disposition { Id = innerElement.Value }; break;
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
                        innerElement.ParseIlmd(epcisEvent); break;
                    case "parentID":
                        epcisEvent.Epcs.Add(new Epc { Id = innerElement.Value, Type = EpcType.ParentId }); break;
                    case "recordTime": // We don't process record time as it will be overrided in any case..
                        break;
                    default:
                        AddToCustomFields(innerElement, epcisEvent); break;
                }
            }
        }

        private static void AddToCustomFields(XElement element, EpcisEvent epcisEvent)
        {
            if (element.Name.Namespace == XNamespace.None || element.Name.Namespace == XNamespace.Xmlns)
            {
                throw new EpcisException($"Element '{element.Name.LocalName}' with namespace '{element.Name.NamespaceName}' not expected here.");
            }

            var field = new CustomField
            {
                Type = FieldType.EventExtension,
                Name = element.Name.LocalName,
                Namespace = element.Name.NamespaceName,
                Value = element.Value
            };

            epcisEvent.CustomFields.Add(field);
        }
    }
}

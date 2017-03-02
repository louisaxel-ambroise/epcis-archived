using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Epcis.Infrastructure.Aop.Log;
using Epcis.Model.Events;

namespace Epcis.Services.Capture.Parsing
{
    public class XmlElementEventParser : IEventParser<XElement>
    {
        [LogMethodCall]
        public virtual IEnumerable<EpcisEvent> Parse(XElement input)
        {
            return ParseEvents(input);
        }

        private static IEnumerable<EpcisEvent> ParseEvents(XContainer input)
        {
            var events = new List<EpcisEvent>();

            foreach (var element in input.Elements())
            {
                var name = element.Name.LocalName;

                if(name == "extension") events.AddRange(ParseEvents(element));
                else events.Add(ParseEvent(element));
            }

            return events;
        }

        private static EpcisEvent ParseEvent(XElement element)
        {
            var epcisEvent = new EpcisEvent{ EventType = element.ToEventType() };

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
                        epcisEvent.EventTimezoneOffset = new TimeZoneOffset(innerElement.Value); break;
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
                        epcisEvent.BusinessStep = innerElement.Value; break;
                    case "disposition":
                        epcisEvent.Disposition = innerElement.Value; break;
                    case "eventId":
                        epcisEvent.EventId = innerElement.Value; break;
                    case "errorDeclaration":
                        epcisEvent.ErrorDeclaration = innerElement.ToErrorDeclaration(); break;
                    case "transformationId":
                        epcisEvent.TransformationId = innerElement.Value; break;
                    case "bizLocation":
                        epcisEvent.BusinessLocation = innerElement.ToBusinessLocation(); break;
                    case "bizTransactionList":
                        epcisEvent.BusinessTransactions = innerElement.ToBusinessTransactions(); break;
                    case "readPoint":
                        epcisEvent.ReadPoint = innerElement.ToReadPoint(); break;
                    case "ilmd":
                        epcisEvent.Ilmd = new XDocument(innerElement); break;
                    case "parentID":
                        epcisEvent.Epcs.Add(new Epc{ Id = innerElement.Value, Type = EpcType.ParentId }); break;
                    case "recordTime": // We don't process record time as it will be overrided in any case..
                        break;
                    default:
                        epcisEvent.CustomFields = AddToCustomFields(innerElement, epcisEvent.CustomFields); break;
                }
            }
        }

        private static XDocument AddToCustomFields(XElement innerElement, XDocument customFields)
        {
            if (customFields == null) customFields = new XDocument(new XElement("root"));
            if (customFields.Root != null) customFields.Root.Add(innerElement);
            
            return customFields;
        }
    }
}

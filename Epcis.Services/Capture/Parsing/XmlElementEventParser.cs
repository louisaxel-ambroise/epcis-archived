using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Epcis.Infrastructure.Aop.Log;
using Epcis.Model;
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

                if(name == Field.Extension) events.AddRange(ParseEvents(element));
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
                    case Field.Extension:
                        ParseAttributes(innerElement, epcisEvent); break;
                    case Field.Action:
                        epcisEvent.Action = innerElement.ToEventAction(); break;
                    case Field.EventTimeZoneOffset:
                        epcisEvent.EventTimezoneOffset = new TimeZoneOffset(innerElement.Value); break;
                    case Field.EventTime:
                        epcisEvent.EventTime = DateTime.Parse(innerElement.Value); break;
                    case Field.EpcList:
                        innerElement.ParseEpcListInto(epcisEvent.Epcs); break;
                    case Field.ChildEpcs:
                        innerElement.ParseChildEpcListInto(epcisEvent.Epcs); break;
                    case Field.InputQuantityList:
                        innerElement.ParseQuantityListInto(epcisEvent.Epcs, true); break;
                    case Field.InputEpcList:
                        innerElement.ParseEpcListInto(epcisEvent.Epcs, true); break;
                    case Field.OutputQuantityList:
                        innerElement.ParseQuantityListInto(epcisEvent.Epcs, false); break;
                    case Field.OutputEpcList:
                        innerElement.ParseEpcListInto(epcisEvent.Epcs, false); break;
                    case Field.EpcClass:
                        epcisEvent.Epcs.Add(new Epc { Type = EpcType.Quantity, Id = innerElement.Value, IsQuantity = true }); break;
                    case Field.Quantity:
                        epcisEvent.Epcs.Single(x => x.Type == EpcType.Quantity).Quantity = float.Parse(innerElement.Value); break;
                    case Field.BusinessStep:
                        epcisEvent.BusinessStep = innerElement.Value; break;
                    case Field.Disposition:
                        epcisEvent.Disposition = innerElement.Value; break;
                    case Field.EventId:
                        epcisEvent.EventId = innerElement.Value; break;
                    case Field.ErrorDeclaration:
                        epcisEvent.ErrorDeclaration = innerElement.ToErrorDeclaration(); break;
                    case Field.TransformationId:
                        epcisEvent.TransformationId = innerElement.Value; break;
                    case Field.BusinessLocation:
                        epcisEvent.BusinessLocation = innerElement.ToBusinessLocation(); break;
                    case Field.BusinessTransactions:
                        epcisEvent.BusinessTransactions = innerElement.ToBusinessTransactions(); break;
                    case Field.ReadPoint:
                        epcisEvent.ReadPoint = innerElement.ToReadPoint(); break;
                    case Field.Ilmd:
                        /* TODO: parse ILMD */ break;
                    case Field.ParentId:
                        epcisEvent.Epcs.Add(new Epc{ Id = innerElement.Value, Type = EpcType.ParentId }); break;
                    case Field.RecordTime: // We don't process record time as it will be overrided in any case..
                        break;
                    default:
                        epcisEvent.CustomFields.Add(innerElement.ToCustomField()); break;
                }
            }
        }
    }
}

using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Epcis.Domain.Exceptions;
using Epcis.Domain.Model.Epcis;
using Epcis.Domain.Services.Mapping;

namespace Epcis.XmlParser.Parsing
{
    public class DocumentParser : IDocumentParser
    {
        private readonly IEventMapper _eventMapper;

        public DocumentParser(IEventMapper eventMapper)
        {
            if (eventMapper == null) throw new ArgumentNullException("eventMapper");

            _eventMapper = eventMapper;
        }

        public BaseEvent[] ParseEvents(XDocument document)
        {
            var eventList = document.Root.Element("EPCISBody").Element("EventList");

            return eventList.Elements().Select(ParseSingleEvent).ToArray();
        }

        private BaseEvent ParseSingleEvent(XElement eventElement)
        {
            var parameters = new EventParameters { Type = eventElement.Name.LocalName };

            foreach (var element in eventElement.Elements())
            {
                if (element.NodeType == XmlNodeType.Comment) continue;

                switch (element.Name.LocalName.ToLower())
                {
                    case "eventtime":
                        parameters.EventTime = element.Value;
                        break;
                    case "eventtimezoneoffset":
                        parameters.EventTimezoneOffset = element.Value;
                        break;
                    case "action":
                        parameters.Action = element.Value;
                        break;
                    case "bizlocation":
                        AddBusinessLocation(element, parameters);
                        break;
                    case "bizstep":
                        parameters.BusinessStep = element.Value;
                        break;
                    case "disposition":
                        parameters.Disposition = element.Value;
                        break;
                    case "readpoint":
                        AddReadPoint(element, parameters);
                        break;
                    case "epclist":
                        AddEpcs(element.Elements().ToArray(), parameters);
                        break;
                    case "parentid":
                        parameters.ParentId = element.Value;
                        break;
                    case "childepcs":
                        AddChildEpcs(element.Elements().ToArray(), parameters);
                        break;
                    default:
                        TryAddCustomElement(element, parameters);
                        break;
                }
            }

            return _eventMapper.MapEvent(parameters);
        }

        private void AddReadPoint(XElement element, EventParameters parameters)
        {
            parameters.ReadPoint = element.Element("id").Value;
        }

        private void AddBusinessLocation(XElement element, EventParameters parameters)
        {
            parameters.BusinessLocation = element.Element("id").Value;
        }

        private void AddEpcs(XElement[] elements, EventParameters parameters)
        {
            if (elements == null || !elements.Any()) return;
            parameters.Epcs = elements.Where(x => x.Name.LocalName.Equals("Epc", StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToArray();
        }

        private void AddChildEpcs(XElement[] elements, EventParameters parameters)
        {
            if (elements == null || !elements.Any()) return;
            parameters.ChildEpcs = elements.Where(x => x.Name.LocalName.Equals("Epc", StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToArray();
        }

        private void TryAddCustomElement(XElement element, EventParameters parameters)
        {
            if(string.IsNullOrEmpty(element.Name.NamespaceName)) return;
            parameters.Extensions.Add(string.Join("#", element.Name.NamespaceName, element.Name.LocalName), element.Value);
        }
    }
}
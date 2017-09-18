using System.Collections.Generic;
using System.Xml.Linq;
using System;
using FasTnT.Domain.Model.Events;
using System.Linq;

namespace FasTnT.Domain.Services.Formatting
{
    public class ResponseFormatter : IResponseFormatter
    {
        private readonly IEventFormatter _eventFormatter;

        public ResponseFormatter(IEventFormatter eventFormatter)
        {
            _eventFormatter = eventFormatter ?? throw new ArgumentException(nameof(eventFormatter));
        }

        public XDocument FormatPollResponse(string queryName, IEnumerable<EpcisEvent> events)
        {
            var epcisQueryNamespace = XNamespace.Get("urn:epcglobal:epcis-query:xsd:1");
            var formatted = events.Select(e => _eventFormatter.Format(e));

            return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(epcisQueryNamespace + "QueryResult",
                    new XAttribute(XNamespace.Xmlns + "a", epcisQueryNamespace),
                    new XElement("queryName", queryName),
                    new XElement("resultBody", new XElement("EventList", formatted.ToArray<object>()))
                )
            );
        }
    }
}

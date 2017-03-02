using System;
using System.Xml.Linq;
using Epcis.Data.Storage;
using Epcis.Infrastructure.Aop.Database;
using Epcis.Services.Capture.Parsing;
using Epcis.Services.Capture.Validation;

namespace Epcis.Services.Capture
{
    public class XmlDocumentCapturer : ICapturer<XDocument>
    {
        private readonly IValidator<XDocument> _validator;
        private readonly IEventParser<XElement> _xmlEventParser;
        private readonly IEventStore _eventStore;

        public XmlDocumentCapturer(IValidator<XDocument> validator, IEventParser<XElement> xmlEventParser, IEventStore eventStore)
        {
            if (validator == null) throw new ArgumentNullException("validator");
            if (xmlEventParser == null) throw new ArgumentNullException("xmlEventParser");
            if (eventStore == null) throw new ArgumentNullException("eventStore");

            _validator = validator;
            _xmlEventParser = xmlEventParser;
            _eventStore = eventStore;
        }

        [CommitTransaction]
        public virtual void Capture(XDocument document)
        {
            _validator.Validate(document);

            if (document.Root == null) return;

            var bodyNode = document.Root.Element("EPCISBody");
            if (bodyNode == null || bodyNode.Element("EventList") == null) return;

            var events = _xmlEventParser.Parse(bodyNode.Element("EventList"));

            foreach (var @event in events)
            {
                _eventStore.Store(@event);
            }
        }
    }
}
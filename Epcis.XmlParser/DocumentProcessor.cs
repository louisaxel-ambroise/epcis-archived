using System;
using System.Xml.Linq;
using Epcis.Domain.Services.Capture.Events;
using Epcis.XmlParser.Parsing;

namespace Epcis.XmlParser
{
    // TODO: add masterData processing
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly IEventsCapturer _eventsCapturer;
        private readonly IDocumentParser _documentParser;

        public DocumentProcessor(IEventsCapturer eventsCapturer, IDocumentParser documentParser)
        {
            if (eventsCapturer == null) throw new ArgumentNullException(nameof(eventsCapturer));
            if (documentParser == null) throw new ArgumentNullException(nameof(documentParser));

            _eventsCapturer = eventsCapturer;
            _documentParser = documentParser;
        }

        public void Process(XDocument document)
        {
            var root = document.Root;

            if (root == null) throw new Exception("TODO: make it BadRequestException?");

            if (root.Name.LocalName.Equals("EpcisDocument", StringComparison.InvariantCultureIgnoreCase))
            {
                ProcessEvents(document);
            }
        }

        private void ProcessEvents(XDocument document)
        {
            var events = _documentParser.ParseEvents(document);

            _eventsCapturer.CaptureEvents(events);
        }
    }
}
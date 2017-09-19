using FasTnT.Domain.Services.Validation;
using FasTnT.Domain.Utils.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.EventCapture
{
    public class EventCapturer : IEventCapturer
    {
        private readonly IDocumentValidator _documentValidator;
        private readonly IDocumentParser _documentParser;
        private readonly IEventPersister _eventPersister;

        public EventCapturer(IDocumentValidator documentValidator, IDocumentParser documentParser, IEventPersister eventPersister)
        {
            _documentValidator = documentValidator ?? throw new ArgumentException(nameof(documentValidator));
            _documentParser = documentParser ?? throw new ArgumentException(nameof(documentParser));
            _eventPersister = eventPersister ?? throw new ArgumentException(nameof(eventPersister));
        }

        [CommitTransaction]
        public virtual IEnumerable<Guid> Capture(XDocument xmlDocument)
        {
            _documentValidator.Validate(xmlDocument);

            var events = _documentParser.Parse(xmlDocument.Root);

            foreach(var @event in events)
            {
                _eventPersister.Persist(@event);
            }

            return events.Select(e => e.Id);
        }
    }
}
using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Services.Users;
using FasTnT.Domain.Services.Validation;
using FasTnT.Domain.Utils;
using FasTnT.Domain.Utils.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Capture.Events
{
    public class EventCapturer : IEventCapturer
    {
        private readonly IDocumentValidator _documentValidator;
        private readonly IDocumentParser _documentParser;
        private readonly IRequestPersister _requestPersister;
        private readonly IUserProvider _userProvider;

        public EventCapturer(IDocumentValidator documentValidator, IDocumentParser documentParser, IRequestPersister requestPersister, IUserProvider userProvider)
        {
            _documentValidator = documentValidator ?? throw new ArgumentException(nameof(documentValidator));
            _documentParser = documentParser ?? throw new ArgumentException(nameof(documentParser));
            _requestPersister = requestPersister ?? throw new ArgumentException(nameof(requestPersister));
            _userProvider = userProvider ?? throw new ArgumentException(nameof(userProvider));
        }

        [CommitTransaction]
        public virtual IEnumerable<string> Capture(XDocument xmlDocument)
        {
            _documentValidator.Validate(xmlDocument);

            var events = _documentParser.Parse(xmlDocument.Root);
            var currentUser = _userProvider.GetCurrentUser();

            var request = new EpcisRequest
            {
                RecordTime = SystemContext.Clock.Now,
                DocumentTime = DateTime.Parse(xmlDocument.Root.Attribute("creationDate").Value),
                User = currentUser
            };

            foreach(var @event in events) request.AddEvent(@event);

            _requestPersister.Persist(request);

            return request.Events.Select(e => e.Id.ToString());
        }
    }
}
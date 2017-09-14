using System;
using System.ServiceModel;
using System.Xml.Linq;
using FasTnT.Web.EpcisServices.Faults;
using FasTnT.Domain.Services.EventCapture;

namespace FasTnT.Web.EpcisServices
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    public class CaptureService : ICaptureService
    {
        private readonly IDocumentParser _documentParser;
        private readonly IEventCapturer _eventCapturer;

        public CaptureService(IDocumentParser documentParser, IEventCapturer eventCapturer)
        {
            _documentParser = documentParser ?? throw new ArgumentNullException(nameof(documentParser));
            _eventCapturer = eventCapturer ?? throw new ArgumentNullException(nameof(eventCapturer));
        }

        public string Capture()
        {
            try
            {
                var request = OperationContext.Current.RequestContext.RequestMessage.ToString() ?? "";
                var document = XDocument.Parse(request);

                var events = _documentParser.Parse(document.Root);
                var eventIds = _eventCapturer.Capture(events);

                return "OK";
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }
    }
}

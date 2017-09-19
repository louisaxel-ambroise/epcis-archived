using System;
using System.ServiceModel;
using System.Xml.Linq;
using FasTnT.Web.EpcisServices.Faults;
using FasTnT.Domain.Services.EventCapture;
using FasTnT.Domain.Utils.Aspects;
using System.Collections.Generic;

namespace FasTnT.Web.EpcisServices
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    public class CaptureService : ICaptureService
    {
        private readonly IEventCapturer _eventCapturer;

        public CaptureService(IDocumentParser documentParser, IEventCapturer eventCapturer)
        {
            _eventCapturer = eventCapturer ?? throw new ArgumentNullException(nameof(eventCapturer));
        }

        [CaptureLog]
        public virtual IEnumerable<Guid> Capture()
        {
            try
            {
                var request = OperationContext.Current.RequestContext.RequestMessage.ToString() ?? "";
                var document = XDocument.Parse(request);

                var eventIds = _eventCapturer.Capture(document);

                return eventIds;
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }
    }
}

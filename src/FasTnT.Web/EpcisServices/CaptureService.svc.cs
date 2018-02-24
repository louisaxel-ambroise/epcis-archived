using System;
using System.ServiceModel;
using System.Xml.Linq;
using FasTnT.Domain.Services.EventCapture;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Web.EpcisServices.Model;
using System.Linq;
using FasTnT.Domain.Utils;
using FasTnT.Web.Helpers.Attributes;

namespace FasTnT.Web.EpcisServices
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    public class CaptureService : ICaptureService
    {
        private readonly IEventCapturer _eventCapturer;

        public CaptureService(IEventCapturer eventCapturer)
        {
            _eventCapturer = eventCapturer ?? throw new ArgumentNullException(nameof(eventCapturer));
        }

        [CaptureLog]
        [AuthenticateUser]
        public virtual CaptureEventsResponse CaptureEvents()
        {
            var captureStart = SystemContext.Clock.Now;

            try
            {
                var request = OperationContext.Current.RequestContext.RequestMessage.ToString() ?? "";
                var document = XDocument.Parse(request);
                var response = _eventCapturer.Capture(document);

                return new CaptureEventsResponse
                {
                    CaptureStart = captureStart,
                    CaptureEnd = SystemContext.Clock.Now,
                    EventsCount = response.Count(),
                    EventIds = response.ToArray()
                };
            }
            catch
            {
                throw new Exception("Capture of events failed");
            }
        }

        public CaptureMasterDataResponse CaptureMasterdata()
        {
            // TODO: parse and capture the masterdata contained in the body.
            throw new Exception("Method CaptureMasterdata is not implemented.");
        }
    }
}

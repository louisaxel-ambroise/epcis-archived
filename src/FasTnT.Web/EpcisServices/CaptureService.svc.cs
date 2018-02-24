using System;
using System.ServiceModel;
using System.Xml.Linq;
using FasTnT.Domain.Services.Capture.Events;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Web.EpcisServices.Model;
using System.Linq;
using FasTnT.Domain.Utils;
using FasTnT.Web.Helpers.Attributes;
using FasTnT.Domain.Services.Capture.Masterdata;

namespace FasTnT.Web.EpcisServices
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    public class CaptureService : ICaptureService
    {
        private readonly IEventCapturer _eventCapturer;
        private readonly IMasterdataCapturer _masterdataCapturer;

        public CaptureService(IEventCapturer eventCapturer, IMasterdataCapturer masterdataCapturer)
        {
            _eventCapturer = eventCapturer ?? throw new ArgumentNullException(nameof(eventCapturer));
            _masterdataCapturer = masterdataCapturer ?? throw new ArgumentNullException(nameof(masterdataCapturer));
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

        [CaptureLog]
        [AuthenticateUser]
        public virtual CaptureMasterDataResponse CaptureMasterdata()
        {
            var captureStart = SystemContext.Clock.Now;

            try
            {
                var request = OperationContext.Current.RequestContext.RequestMessage.ToString() ?? "";
                var document = XDocument.Parse(request);
                var masterdataIds = _masterdataCapturer.Capture(document);

                return new CaptureMasterDataResponse
                {
                    CaptureStart = captureStart,
                    CaptureEnd = SystemContext.Clock.Now,
                    MasterdataCount = masterdataIds.Count(),
                    MasterdataIds = masterdataIds.ToArray()
                };
            }
            catch
            {
                throw new Exception("Capture of master data failed");
            }
        }
    }
}

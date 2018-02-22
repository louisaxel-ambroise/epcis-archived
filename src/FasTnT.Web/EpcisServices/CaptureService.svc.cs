using System;
using System.ServiceModel;
using System.Xml.Linq;
using FasTnT.Domain.Services.EventCapture;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Domain.Model.Capture;

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
        public virtual CaptureResponse CaptureEvents()
        {
            try
            {
                var request = OperationContext.Current.RequestContext.RequestMessage.ToString() ?? "";
                var document = XDocument.Parse(request);
                var response = _eventCapturer.Capture(document);

                return response;
            }
            catch
            {
                throw new Exception("Capture of events failed");
            }
        }

        public CaptureResponse CaptureMasterdata()
        {
            // TODO: parse and capture the masterdata contained in the body.
            throw new Exception("Method CaptureMasterdata is not implemented.");
        }
    }
}

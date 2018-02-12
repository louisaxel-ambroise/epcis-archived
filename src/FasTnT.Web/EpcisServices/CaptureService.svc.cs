using System;
using System.ServiceModel;
using System.Xml.Linq;
using FasTnT.Web.EpcisServices.Faults;
using FasTnT.Domain.Services.EventCapture;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Domain.Model.Capture;
using FasTnT.Web.Helpers.Attributes;

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
        [AuthenticateUser]
        public virtual CaptureResponse Capture()
        {
            try
            {
                var request = OperationContext.Current.RequestContext.RequestMessage.ToString() ?? "";
                var document = XDocument.Parse(request);
                var response = _eventCapturer.Capture(document);

                return response;
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }
    }
}

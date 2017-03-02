using System;
using System.ServiceModel;
using System.Xml.Linq;
using Epcis.Api.Faults;
using Epcis.Services.Capture;

namespace Epcis.Api.Services
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)] 
    public class CaptureService : ICaptureService
    {
        private readonly ICapturer<XDocument> _xmlDocumentProcessor;

        public CaptureService(ICapturer<XDocument> xmlDocumentProcessor)
        {
            if (xmlDocumentProcessor == null) throw new ArgumentNullException("xmlDocumentProcessor");

            _xmlDocumentProcessor = xmlDocumentProcessor;
        }

        public string Capture()
        {
            try
            {
                var message = OperationContext.Current.RequestContext.RequestMessage.ToString() ?? "";
                var document = XDocument.Parse(message);

                _xmlDocumentProcessor.Capture(document);

                return "OK";
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }
    }
}

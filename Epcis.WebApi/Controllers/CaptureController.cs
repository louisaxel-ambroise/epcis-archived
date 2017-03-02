using System;
using System.Web.Http;
using System.Xml.Linq;
using Epcis.Services.Capture;
using Epcis.WebApi.Infrastructure;

namespace Epcis.WebApi.Controllers
{
    public class CaptureController : ApiController
    {
        private readonly ICapturer<XDocument> _xmlDocumentProcessor;

        public CaptureController(ICapturer<XDocument> xmlDocumentProcessor)
        {
            if (xmlDocumentProcessor == null) throw new ArgumentNullException("xmlDocumentProcessor");

            _xmlDocumentProcessor = xmlDocumentProcessor;
        }

        [HttpPost]
        [Route("api/1.2/capture")]
        public IHttpActionResult Capture([XmlBody] XDocument input)
        {
            try
            {
                _xmlDocumentProcessor.Capture(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
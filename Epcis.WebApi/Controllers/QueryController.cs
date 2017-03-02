using System;
using System.Web.Http;
using System.Xml.Linq;
using Epcis.Model.Query;
using Epcis.Services.Query;
using Epcis.WebApi.Infrastructure;

namespace Epcis.WebApi.Controllers
{
    public class QueryController : ApiController
    {
        private readonly IEventQuery<XDocument> _eventQuery;

        public QueryController(IEventQuery<XDocument> eventQuery)
        {
            if (eventQuery == null) throw new ArgumentNullException("eventQuery");

            _eventQuery = eventQuery;
        }

        [HttpPost]
        [Route("api/1.2/query")]
        public IHttpActionResult Query([XmlSoapBody] EpcisQuery<XElement> body)
        {
            return Ok(_eventQuery.Query(body));
        }
    }
}
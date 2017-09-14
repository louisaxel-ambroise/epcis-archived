using System.Collections.Generic;
using System.Xml.Linq;
using System;
using FasTnT.Domain.Model.Events;

namespace FasTnT.Domain.Services.Formatting
{
    public class ResponseFormatter : IResponseFormatter
    {
        public XDocument FormatResponse(IEnumerable<EpcisEvent> events)
        {
            throw new NotImplementedException();
        }
    }
}

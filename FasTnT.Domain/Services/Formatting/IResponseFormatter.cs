using FasTnT.Domain.Model.Events;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Formatting
{
    public interface IResponseFormatter
    {
        XDocument FormatPollResponse(string queryName, IEnumerable<EpcisEvent> events);
    }
}

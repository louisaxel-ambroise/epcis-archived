using System.Xml.Linq;
using FasTnT.Domain.Model.Queries;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Formatting
{
    public interface IEventFormatter
    {
        IEnumerable<XElement> Format(QueryEventResponse eventResponse);
    }
}

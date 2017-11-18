using FasTnT.Domain.Model.Queries;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Formatting
{
    public interface IResponseFormatter
    {
        XDocument FormatPollResponse(string queryName, QueryEventResponse events);
        XDocument FormatSubscriptionResponse(string queryName, string subscriptionName, QueryEventResponse events);
    }
}

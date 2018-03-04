using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;
using FasTnT.Domain.Utils;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Formatting
{
    public class XmlResponseFormatter : IResponseFormatter
    {
        public const string EpcisQueryNamespace = "urn:epcglobal:epcis-query:xsd:1";
        private readonly IEventFormatter _eventFormatter;

        public XmlResponseFormatter(IEventFormatter eventFormatter)
        {
            _eventFormatter = eventFormatter;
        }

        public string FormatSubscriptionResponse(Subscription subscription, QueryEventResponse response)
        {
            var root = new XElement(XName.Get("EPCISQueryDocument", EpcisQueryNamespace));
            root.Add(new XAttribute("creationDate", SystemContext.Clock.Now));
            root.Add(new XAttribute("schemaVersion", "1.0"));

            var body = new XElement("EPCISBody");
            var queryResults = new XElement(XName.Get("QueryResults", EpcisQueryNamespace));
            queryResults.Add(new XElement("queryName", subscription.QueryName));
            queryResults.Add(new XElement("subscriptionID", subscription.Id));

            var eventList = new XElement("resultsBody", new XElement("EventList", _eventFormatter.Format(response)));

            queryResults.Add(eventList);
            body.Add(queryResults);
            root.Add(body);

            return root.ToString(SaveOptions.DisableFormatting);
        }
    }
}

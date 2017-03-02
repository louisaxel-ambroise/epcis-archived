using System;
using System.Xml.Linq;
using Epcis.Model.Subscriptions;

namespace Epcis.Api.Model
{
    public static class SubscriptionRequest
    {
        public static Subscription Parse(XElement xmlRequest)
        {
            var subscription = new Subscription();

            foreach (var element in xmlRequest.Elements())
            {
                if (element.Name.LocalName == "destination") subscription.Destination = element.Value;
                if (element.Name.LocalName == "queryName") subscription.QueryName = element.Value;
                if (element.Name.LocalName == "id") subscription.Id = element.Value;
                if (element.Name.LocalName == "params") subscription.Parameters = PollRequest.ParseParameters(element);
                if (element.Name.LocalName == "controls") subscription.Controls = ParseControls(element);
            }

            return subscription;
        }

        // TODO: parse parameters.
        private static SubscriptionControls ParseControls(XElement element)
        {
            return new SubscriptionControls
            {
                InitialRecordTime = DateTime.UtcNow,
                ReportIfEmpty = true
            };
        }
    }
}
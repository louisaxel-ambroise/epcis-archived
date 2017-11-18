using FasTnT.Domain.Model.Subscriptions;
using FasTnT.Domain.Utils;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Web.EpcisServices.Model
{
    public static class SubscriptionRequest
    {
        public static Subscription Parse(XElement xmlRequest)
        {
            var subscription = new Subscription();

            foreach (var element in xmlRequest.Elements())
            {
                if (element.Name.LocalName == "destination") subscription.DestinationUrl = element.Value;
                if (element.Name.LocalName == "queryName") subscription.QueryName = element.Value;
                if (element.Name.LocalName == "id") subscription.Id = element.Value;
                if (element.Name.LocalName == "params") subscription.Parameters = ParseParameters(element);
                if (element.Name.LocalName == "controls") subscription.Controls = ParseControls(element);
            }

            return subscription;
        }

        private static IList<SubscriptionParameter> ParseParameters(XElement element)
        {
            var parameters = new List<SubscriptionParameter>();

            foreach (var parameter in element.Elements().Where(x => x.Name.LocalName == "param"))
            {
                var param = new SubscriptionParameter();

                foreach (var elt in parameter.Elements())
                {
                    if (elt.Name.LocalName == "name") param.ParameterName = elt.Value;
                    else if (elt.Name.LocalName == "value") param.Values.Add(new SubscriptionParameterValue { Parameter = param, Value = elt.Value });
                    else throw new Exception($"Element '{elt.Name.LocalName}' is not expected here.");
                }

                parameters.Add(param);
            }

            return parameters.ToArray();
        }

        // TODO: parse parameters.
        private static SubscriptionControls ParseControls(XElement element)
        {
            return new SubscriptionControls
            {
                InitialRecordTime = SystemContext.Clock.Now,
                ReportIfEmpty = true
            };
        }
    }
}
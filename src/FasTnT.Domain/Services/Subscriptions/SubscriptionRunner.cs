using FasTnT.Domain.Services.Queries.Performers;
using FasTnT.Domain.Services.Formatting;
using FasTnT.Domain.Model.Subscriptions;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Net;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Domain.Utils;

namespace FasTnT.Domain.Services.Subscriptions
{
    public class SubscriptionRunner : ISubscriptionRunner
    {
        private readonly IQueryPerformer _queryPerformer;
        private readonly IResponseFormatter _responseFormatter;

        public SubscriptionRunner(IQueryPerformer queryPerformer, IResponseFormatter responseFormatter)
        {
            _queryPerformer = queryPerformer;
            _responseFormatter = responseFormatter;
        }

        [CommitTransaction]
        public virtual async Task Run(Subscription subscription)
        {
            Trace.WriteLine($"Running subscription {subscription.Name}");
            var events = _queryPerformer.ExecuteSubscriptionQuery(subscription);

            subscription.LastRunOn = SystemContext.Clock.Now;

            if (events.Events.Count() > 0 || subscription.Controls.ReportIfEmpty)
            {
                var response = _responseFormatter.FormatSubscriptionResponse(subscription.QueryName, subscription.Name, events);

                await SendResponse(subscription, response);
            }
            Trace.WriteLine($"Finished running subscription {subscription.Name}");
        }

        private async Task SendResponse(Subscription subscription, XDocument response)
        {
            var client = WebRequest.Create(subscription.DestinationUrl) as HttpWebRequest;
            client.Method = "POST";
            using (var stream = client.GetRequestStream())
            {
                var bytes = Encoding.UTF8.GetBytes(response.ToString(SaveOptions.DisableFormatting));
                stream.Write(bytes, 0 , bytes.Length);
            }

            var httpResponse = (await client.GetResponseAsync()) as HttpWebResponse;
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                Trace.WriteLine($"Woops.");
            }
        }
    }
}

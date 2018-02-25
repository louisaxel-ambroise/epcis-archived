using FasTnT.Domain.Services.Queries.Performers;
using FasTnT.Domain.Model.Subscriptions;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Net;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Domain.Utils;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Services.Formatting;

namespace FasTnT.Domain.Services.Subscriptions
{
    public class SubscriptionRunner : ISubscriptionRunner
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IQueryPerformer _queryPerformer;
        private readonly IResponseFormatter _responseFormatter;

        public SubscriptionRunner(ISubscriptionRepository subscriptionRepository, IQueryPerformer queryPerformer, IResponseFormatter responseFormatter)
        {
            _subscriptionRepository = subscriptionRepository;
            _queryPerformer = queryPerformer;
            _responseFormatter = responseFormatter;
        }

        [CommitTransaction]
        public virtual void Run(string subscriptionId)
        {
            var subscription = _subscriptionRepository.LoadById(subscriptionId);

            Trace.WriteLine($"Running subscription {subscription.Id}");
            var events = _queryPerformer.ExecuteSubscriptionQuery(subscription);

            subscription.LastRunOn = SystemContext.Clock.Now;
            _subscriptionRepository.DeletePendingRequests(subscription.PendingRequests);

            if (events.Events.Count() > 0 || subscription.Controls.ReportIfEmpty)
            {
                var response = _responseFormatter.FormatSubscriptionResponse(subscription, events);

                SendResponse(subscription, response);
            }
            Trace.WriteLine($"Finished running subscription {subscription.Id}");
        }

        private void SendResponse(Subscription subscription, string response)
        {
            var client = WebRequest.Create(subscription.DestinationUrl) as HttpWebRequest;
            client.Method = "POST";
            client.ContentType = "text/xml";

            using (var stream = client.GetRequestStream())
            {
                var bytes = Encoding.UTF8.GetBytes(response);
                stream.Write(bytes, 0 , bytes.Length);
            }

            var httpResponse = (client.GetResponseAsync().Result) as HttpWebResponse;
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                Trace.WriteLine($"Woops.");
            }
        }
    }
}

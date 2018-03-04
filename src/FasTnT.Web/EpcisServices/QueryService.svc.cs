using System.Linq;
using System.Reflection;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Domain.Services.Queries.Performers;
using FasTnT.Domain.Services.Queries;
using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Services.Formatting;
using FasTnT.Web.Helpers.Attributes;
using FasTnT.Domain.Services.Subscriptions;
using FasTnT.Web.EpcisServices.Mappings;

namespace FasTnT.Web.EpcisServices
{
    /// <summary>
    /// Implementation of the IQueryService SOAP WebService.
    /// </summary>
    public class QueryService : IQueryService
    {
        private readonly IEventFormatter _eventFormatter;
        private readonly IQueryPerformer _queryPerformer;
        private readonly IQueryManager _queryManager;
        private readonly ISubscriptionManager _subscriptionManager;

        public QueryService(IQueryPerformer queryPerformer, IQueryManager queryManager, IEventFormatter eventFormatter, ISubscriptionManager subscriptionManager)
        {
            _eventFormatter = eventFormatter;
            _queryPerformer = queryPerformer;
            _queryManager = queryManager;
            _subscriptionManager = subscriptionManager;
        }

        public string GetVendorVersion(EmptyParms request) => Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        public string GetStandardVersion(EmptyParms request) => "1.2";

        [SoapFaultHandler]
        [AuthenticateUser]
        public string[] GetQueryNames(EmptyParms request)
        {
            return _queryManager.ListQueryNames().ToArray();
        }

        [QueryLog]
        [SoapFaultHandler]
        [AuthenticateUser]
        public virtual QueryResults Poll(PollRequest request)
        {
            var queryParameters = request.parameters?.Select(x => new QueryParam { Name = x.Name, Values = x.Values });
            var results = _queryPerformer.ExecutePollQuery(request.QueryName, queryParameters);
            var formattedResponse = _eventFormatter.Format(results);

            return new QueryResults { QueryName = request.QueryName, EventList = new EventList { Elements = formattedResponse } };
        }

        [SoapFaultHandler]
        [AuthenticateUser]
        public virtual string[] GetSubscriptionIDs(EmptyParms request)
        {
            return _subscriptionManager.ListAllSubscriptions().Select(x => x.Name).ToArray();
        }

        [SoapFaultHandler]
        [AuthenticateUser]
        public virtual SubscribeResult Subscribe(SubscribeRequest request)
        {
            var @params = request.Parameters.MapToParameters();
            var controls = request.Controls.MapToControls();
            var schedule = request.Controls?.Schedule.MapToSchedule();

            _subscriptionManager.Subscribe(request.SubscriptionId, request.QueryName, @params, request.Destination, controls, schedule);

            return new SubscribeResult { SubscriptionId = request.SubscriptionId };
        }

        [SoapFaultHandler]
        [AuthenticateUser]
        public virtual void Unsubscribe(string subscriptionId)
        {
            _subscriptionManager.Unsubscribe(subscriptionId);
        }
    }
}

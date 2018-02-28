using System;
using System.Linq;
using System.Reflection;
using FasTnT.Web.EpcisServices.Faults;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Domain.Exceptions;
using FasTnT.Domain.Services.Queries.Performers;
using FasTnT.Domain.Services.Queries;
using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Services.Formatting;
using FasTnT.Web.Helpers.Attributes;
using FasTnT.Domain.Services.Subscriptions;

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

        [AuthenticateUser]
        public string[] GetQueryNames(EmptyParms request)
        {
            try
            {
                return _queryManager.ListQueryNames().ToArray();
            }
            catch (EpcisException ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        public string GetVendorVersion(EmptyParms request)
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        }

        public string GetStandardVersion(EmptyParms request)
        {
            return "1.2";
        }

        [QueryLog]
        [AuthenticateUser]
        public virtual QueryResults Poll(PollRequest request)
        {
            try
            {
                var queryParameters = request.parameters?.Select(x => new QueryParam { Name = x.Name, Values = x.Values });
                var results = _queryPerformer.ExecutePollQuery(request.QueryName, queryParameters);
                var formattedResponse = _eventFormatter.Format(results);

                return new QueryResults { QueryName = request.QueryName, EventList = new EventList { Elements = formattedResponse } };
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        [AuthenticateUser]
        public virtual string[] GetSubscriptionIDs(EmptyParms request)
        {
            return _subscriptionManager.ListAllSubscriptions().Select(x => x.Name).ToArray();
        }

        [AuthenticateUser]
        public virtual SubscribeResult Subscribe(SubscribeRequest request)
        {
            var @params = request.Parameters.Select(x => new QueryParam
            {
                Name = x.Name,
                Values = x.Values
            });

            try
            {
                _subscriptionManager.Subscribe(request.QueryName, @params, request.Destination, request.Controls?.ReportIfEmpty ?? false, request.SubscriptionId);

                return new SubscribeResult { SubscriptionId = request.SubscriptionId };
            }
            catch (EpcisException ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        [AuthenticateUser]
        public virtual void Unsubscribe(string subscriptionId)
        {
            try
            {
                _subscriptionManager.Unsubscribe(subscriptionId);
            }
            catch(Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }
    }
}

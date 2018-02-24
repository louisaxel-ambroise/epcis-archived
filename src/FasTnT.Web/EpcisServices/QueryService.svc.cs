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

        public QueryService(IQueryPerformer queryPerformer, IQueryManager queryManager, IEventFormatter eventFormatter)
        {
            _eventFormatter = eventFormatter;
            _queryPerformer = queryPerformer;
            _queryManager = queryManager;
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
            catch (EpcisException ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        [AuthenticateUser]
        public string[] GetSubscriptionIDs(EmptyParms request)
        {
            throw new NotImplementedException();
        }

        [AuthenticateUser]
        public void Subscribe(string queryName/*, QueryParams parameters*/, Uri destination, SubscriptionControls controls, string subscriptionId)
        {
            throw new NotImplementedException();
        }

        [AuthenticateUser]
        public void Unsubscribe(string name)
        {
            throw new NotImplementedException();
        }
    }
}

using FasTnT.Domain.Services.Queries;
using FasTnT.Domain.Services.Queries.Performers;
using System.Linq;
using System.Reflection;
using System;
using System.ServiceModel;

namespace FasTnT.Web.EpcisServices
{
    public class EpcisQueryService : IEpcisQueryService
    {
        private IQueryPerformer _queryPerformer;
        private IQueryManager _queryManager;

        public EpcisQueryService(IQueryPerformer queryPerformer, IQueryManager queryManager)
        {
            _queryPerformer = queryPerformer;
            _queryManager = queryManager;
        }

        public string[] GetQueryNames(EmptyParms request)
        {
            return _queryManager.ListQueryNames().ToArray();
        }

        public string GetVendorVersion([MessageParameter(Name = "VendorVersionRequest")] EmptyParms request)
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        }

        public string GetStandardVersion([MessageParameter(Name = "StandardVersionRequest")] EmptyParms request)
        {
            return "1.2";
        }

        public PollResponse Poll(string queryName, QueryParams parameters)
        {
            return new PollResponse { Text = parameters == null ? "Params are null" : $"Received {parameters.Count} params" };
        }

        public string[] GetSubscriptionIDs([MessageParameter(Name = "SubscriptionIDsRequest")] EmptyParms request)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string name)
        {
            throw new NotImplementedException();
        }
    }
}

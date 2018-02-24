using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Queries.Performers
{
    public interface IQueryPerformer
    {
        QueryEventResponse ExecuteSubscriptionQuery(Subscription subscription);
        QueryEventResponse ExecutePollQuery(string queryName, IEnumerable<QueryParam> parameters);
    }
}
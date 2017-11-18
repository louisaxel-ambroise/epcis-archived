using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;

namespace FasTnT.Domain.Services.Queries.Performers
{
    public interface IQueryPerformer
    {
        QueryEventResponse ExecuteSubscriptionQuery(Subscription subscription);
        QueryEventResponse ExecutePollQuery(string queryName, QueryParam[] parameters);
    }
}
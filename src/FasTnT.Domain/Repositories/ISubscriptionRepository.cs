using FasTnT.Domain.Model.Subscriptions;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Subscription LoadById(string id);
        IQueryable<Subscription> Query();

        void DeletePendingRequests(IEnumerable<SubscriptionPendingRequest> requests);
    }
}

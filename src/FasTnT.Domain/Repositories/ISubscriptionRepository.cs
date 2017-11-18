using FasTnT.Domain.Model.Subscriptions;
using System.Linq;

namespace FasTnT.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        IQueryable<Subscription> Query();
    }
}

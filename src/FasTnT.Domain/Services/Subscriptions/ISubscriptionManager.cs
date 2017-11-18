using FasTnT.Domain.Model.Subscriptions;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Subscriptions
{
    public interface ISubscriptionManager
    {
        IEnumerable<Subscription> ListAllSubscriptions();
    }
}

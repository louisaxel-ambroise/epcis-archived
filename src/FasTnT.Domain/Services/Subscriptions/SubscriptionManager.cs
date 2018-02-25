using FasTnT.Domain.Model.Subscriptions;
using FasTnT.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Subscriptions
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionManager(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
        }

        public IEnumerable<Subscription> ListAllSubscriptions()
        {
            return _subscriptionRepository.Query();
        }
    }
}

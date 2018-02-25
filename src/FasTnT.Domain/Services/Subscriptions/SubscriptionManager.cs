using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Services.Users;
using FasTnT.Domain.Utils;
using FasTnT.Domain.Utils.Aspects;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Subscriptions
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserProvider _userProvider;

        public SubscriptionManager(ISubscriptionRepository subscriptionRepository, IUserProvider userProvider)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public IEnumerable<Subscription> ListAllSubscriptions()
        {
            return _subscriptionRepository.Query();
        }

        [CommitTransaction]
        public virtual void Subscribe(string queryName, IEnumerable<QueryParam> parameters, Uri destination, bool reportIfEmpty, string subscriptionId)
        {
            var currentUser = _userProvider.GetCurrentUser();
            var subscription = _subscriptionRepository.LoadById(subscriptionId);
            if (subscription != null) throw new Exception($"Subscription '{subscriptionId} already exist");

            subscription = new Subscription
            {
                Id = subscriptionId,
                DestinationUrl = destination.ToString(),
                User = currentUser,
                LastRunOn = SystemContext.Clock.Now,
                QueryName = queryName,
                Controls = new SubscriptionControls { ReportIfEmpty = reportIfEmpty },
                Schedule = new SubscriptionSchedule { Seconds = "0" },
                Parameters = null
            };

            _subscriptionRepository.Save(subscription);
        }

        [CommitTransaction]
        public virtual void Unsubscribe(string subscriptionId)
        {
            var currentUser = _userProvider.GetCurrentUser();
            var subscription = _subscriptionRepository.LoadById(subscriptionId);

            if(subscription.User != null && subscription.User.Id != currentUser.Id)
            {
                throw new Exception($"This subscription is assigned to a different user.");
            }

            _subscriptionRepository.Delete(subscription);
        }
    }
}

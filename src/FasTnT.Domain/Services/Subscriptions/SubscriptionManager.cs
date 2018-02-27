using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Services.Queries;
using FasTnT.Domain.Services.Users;
using FasTnT.Domain.Utils;
using FasTnT.Domain.Utils.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Domain.Services.Subscriptions
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserProvider _userProvider;
        private readonly IQuery[] _queries;

        public SubscriptionManager(ISubscriptionRepository subscriptionRepository, IUserProvider userProvider, IQuery[] queries)
        {
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        public IEnumerable<Subscription> ListAllSubscriptions()
        {
            return _subscriptionRepository.Query();
        }

        [CommitTransaction]
        public virtual void Subscribe(string queryName, IEnumerable<QueryParam> parameters, Uri destination, bool reportIfEmpty, string subscriptionId)
        {
            EnsureSubscriptionDoesNotExist(subscriptionId);
            EnsureQueryExistsAndAllowSubscription(queryName);

            var currentUser = _userProvider.GetCurrentUser();
            var subscription = new Subscription
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

        private void EnsureQueryExistsAndAllowSubscription(string queryName)
        {
            var query = _queries.SingleOrDefault(x => x.Name == queryName);
            if (query == null) throw new Exception($"Query '{queryName}' does not exist");
            if (!query.AllowsSubscription) throw new Exception($"Query '{queryName}' does not allow subscriptions");
        }

        private void EnsureSubscriptionDoesNotExist(string subscriptionId)
        {
            var subscription = _subscriptionRepository.LoadById(subscriptionId);
            if (subscription != null) throw new Exception($"Subscription '{subscriptionId} already exist");
        }
    }
}

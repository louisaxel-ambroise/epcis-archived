using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;
using FasTnT.Domain.Model.Users;
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
            var currentUser = _userProvider.GetCurrentUser();

            return _subscriptionRepository.Query().Where(x => x.User.Id == currentUser.Id);
        }

        [CommitTransaction]
        public virtual void Subscribe(string queryName, IEnumerable<QueryParam> parameters, string destination, bool reportIfEmpty, string subscriptionId)
        {
            var currentUser = _userProvider.GetCurrentUser();

            EnsureSubscriptionDoesNotExistForUser(subscriptionId, currentUser);
            EnsureQueryExistsAndAllowSubscription(queryName);
            EnsureDestinationIsValid(destination);

            var subscription = new Subscription
            {
                Name = subscriptionId,
                DestinationUrl = destination?.ToString(),
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
            var subscription = _subscriptionRepository.Query().SingleOrDefault(x => x.Name == subscriptionId && x.User.Id == currentUser.Id);

            if(subscription == null)
            {
                throw new Exception($"Subscription '{subscriptionId}' does not exist");
            }

            _subscriptionRepository.Delete(subscription);
        }

        private void EnsureQueryExistsAndAllowSubscription(string queryName)
        {
            var query = _queries.SingleOrDefault(x => x.Name == queryName);
            if (query == null) throw new Exception($"Query '{queryName}' does not exist");
            if (!query.AllowsSubscription) throw new Exception($"Query '{queryName}' does not allow subscriptions");
        }

        private void EnsureSubscriptionDoesNotExistForUser(string subscriptionId, User user)
        {
            var subscription = _subscriptionRepository.Query().Where(x => x.User.Id == user.Id && x.Name == subscriptionId);
            if (subscription != null) throw new Exception($"Subscription '{subscriptionId}' already exist");
        }

        private void EnsureDestinationIsValid(string destination)
        {
            if (destination == null) throw new Exception($"Subscription destination must be specified");
        }
    }
}

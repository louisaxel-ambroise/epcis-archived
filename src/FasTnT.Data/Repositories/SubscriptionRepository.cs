using FasTnT.Domain.Repositories;
using System;
using System.Linq;
using FasTnT.Domain.Model.Subscriptions;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;

namespace FasTnT.Data.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ISession _session;

        public SubscriptionRepository(ISession session)
        {
            _session = session ?? throw new ArgumentException(nameof(session));
        }

        public void Save(Subscription subscription)
        {
            _session.Save(subscription);
        }

        public Subscription LoadById(Guid id)
        {
            return _session.Get<Subscription>(id);
        }

        public IQueryable<Subscription> Query()
        {
            return _session.Query<Subscription>();
        }

        public void DeletePendingRequests(IEnumerable<SubscriptionPendingRequest> requests)
        {
            foreach(var request in requests)
            {
                _session.Delete(request);
            }
        }

        public void Delete(Subscription subscription)
        {
            _session.Delete(subscription);
        }
    }
}

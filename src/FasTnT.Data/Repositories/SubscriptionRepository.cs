using FasTnT.Domain.Repositories;
using System;
using System.Linq;
using FasTnT.Domain.Model.Subscriptions;
using NHibernate;
using NHibernate.Linq;

namespace FasTnT.Data.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ISession _session;

        public SubscriptionRepository(ISession session)
        {
            _session = session ?? throw new ArgumentException(nameof(session));
        }

        public IQueryable<Subscription> Query()
        {
            return _session.Query<Subscription>();
        }
    }
}

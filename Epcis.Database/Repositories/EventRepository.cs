using System;
using System.Linq;
using Epcis.Domain.Model.Epcis;
using Epcis.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Epcis.Database.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ISession _session;

        public EventRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
        }

        public IQueryable<T> Query<T>() where T : BaseEvent
        {
            return _session.Query<T>();
        }

        public void Store(BaseEvent @event)
        {
            _session.Save(@event);
        }
    }
}
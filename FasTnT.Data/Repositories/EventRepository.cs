using FasTnT.Domain.Repositories;
using System.Linq;
using FasTnT.Domain.Model.Events;
using System;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using FasTnT.Domain.Model;

namespace FasTnT.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ISession _session;

        public EventRepository(ISession session)
        {
            _session = session ?? throw new ArgumentException(nameof(session));
        }

        public EpcisEvent LoadById(Guid eventId)
        {
            return _session.Load<EpcisEvent>(eventId);
        }

        public IQueryable<EpcisEvent> Query()
        {
            return _session.Query<EpcisEvent>();
        }

        public void Insert(EpcisEvent @event)
        {
            _session.Persist(@event);
        }

        public IEnumerable<BusinessTransaction> LoadBusinessTransactions(IEnumerable<EpcisEvent> events)
        {
            return _session.Query<BusinessTransaction>().Where(x => events.Contains(x.Event)).ToFuture();
        }

        public IEnumerable<Epc> LoadEpcs(IEnumerable<EpcisEvent> events)
        {
            return _session.Query<Epc>().Where(x => events.Contains(x.Event)).ToFuture();
        }

        public IEnumerable<CustomField> LoadCustomFields(IEnumerable<EpcisEvent> events)
        {
            return _session.Query<CustomField>().Where(x => events.Contains(x.Event)).ToFuture();
        }

        public IEnumerable<SourceDestination> LoadSourceDestinations(IEnumerable<EpcisEvent> events)
        {
            return _session.Query<SourceDestination>().Where(x => events.Contains(x.Event)).ToFuture();
        }
    }
}

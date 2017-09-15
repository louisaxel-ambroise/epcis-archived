using FasTnT.Domain.Repositories;
using System.Linq;
using FasTnT.Domain.Model.Events;
using System;
using NHibernate;
using NHibernate.Linq;
using FasTnT.Domain.Model.MasterData;

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
            //Load referenced object to avoir database trip
            if (@event.BusinessLocation != null) @event.BusinessLocation = _session.Load<BusinessLocation>(@event.BusinessLocation.Id);
            if (@event.BusinessStep != null) @event.BusinessStep = _session.Load<BusinessStep>(@event.BusinessStep.Id);
            if (@event.Disposition != null) @event.Disposition = _session.Load<Disposition>(@event.Disposition.Id);
            if (@event.ReadPoint != null) @event.ReadPoint = _session.Load<ReadPoint>(@event.ReadPoint.Id);

            _session.Persist(@event);
        }
    }
}

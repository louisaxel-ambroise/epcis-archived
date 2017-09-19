using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Repositories;
using System;

namespace FasTnT.Domain.Services.EventCapture
{
    public class EventPersister : IEventPersister
    {
        private readonly IEventRepository _eventRepository;

        public EventPersister(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentException(nameof(eventRepository));
        }

        public virtual void Persist(EpcisEvent @event)
        {
            _eventRepository.Insert(@event);
        }
    }
}
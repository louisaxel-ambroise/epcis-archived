using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Utils.Aspects;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.EventCapture
{
    public class EventCapturer : IEventCapturer
    {
        private readonly IEventRepository _eventRepository;

        public EventCapturer(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentException(nameof(eventRepository));
        }

        [CommitTransaction]
        public virtual IEnumerable<Guid> Capture(IEnumerable<EpcisEvent> events)
        {
            foreach(var @event in events)
            {
                _eventRepository.Insert(@event);
            }

            return null; // TODO.
        }
    }
}
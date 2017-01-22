using System;
using System.Linq;
using Epcis.Domain.Infrastructure;
using Epcis.Domain.Model.CoreBusinessVocabulary;
using Epcis.Domain.Model.Epcis;
using Epcis.Domain.Repositories;

namespace Epcis.Domain.Services.Capture.Events
{
    public class EventsCapturer : IEventsCapturer
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEpcRepository _epcRepository;
        private readonly ICoreBusinessEntityRepository _cbvRepository;

        public EventsCapturer(IEventRepository eventRepository, IEpcRepository epcRepository, ICoreBusinessEntityRepository cbvRepository)
        {
            if (eventRepository == null) throw new ArgumentNullException(nameof(eventRepository));
            if (epcRepository == null) throw new ArgumentNullException(nameof(epcRepository));
            if (cbvRepository == null) throw new ArgumentNullException(nameof(cbvRepository));

            _eventRepository = eventRepository;
            _epcRepository = epcRepository;
            _cbvRepository = cbvRepository;
        }

        [CommitTransaction]
        public virtual void CaptureEvents(BaseEvent[] events)
        {
            for (var i = 0; i < events.Length; i++)
            {
                ProcessEvent(events[i]);
            }
        }

        private void ProcessEvent(BaseEvent @event)
        {
            @event.BusinessLocation = @event.BusinessLocation != null ? _cbvRepository.LoadWithName<BusinessLocation>(@event.BusinessLocation.Name) : null;
            @event.BusinessStep = @event.BusinessStep != null ? _cbvRepository.LoadWithName<BusinessStep>(@event.BusinessStep.Name) : null;
            @event.ReadPoint = @event.ReadPoint != null ? _cbvRepository.LoadWithName<ReadPoint>(@event.ReadPoint.Name) : null;
            @event.Disposition = @event.Disposition != null ? _cbvRepository.LoadWithName<Disposition>(@event.Disposition.Name) : null;

            if (@event is ObjectEvent) ProcessObjectEvent(@event as ObjectEvent);
            if (@event is AggregationEvent) ProcessAggregationEvent(@event as AggregationEvent);
            if (@event is TransformationEvent) ProcessTransformationEvent(@event as TransformationEvent);

            _eventRepository.Store(@event);
        }

        private void ProcessAggregationEvent(AggregationEvent aggregationEvent)
        {
            if (aggregationEvent.Action == EventAction.ADD)
            {
                foreach (var childEpc in aggregationEvent.ChildEpcs.Select(epc => _epcRepository.Load(epc.Id)))
                {
                    childEpc.Parent = aggregationEvent.Parent;
                }
            }

            if (aggregationEvent.Action == EventAction.DELETE)
            {
                foreach (var childEpc in aggregationEvent.ChildEpcs.Select(epc => _epcRepository.Load(epc.Id)))
                {
                    childEpc.Parent = childEpc.Parent.Id == aggregationEvent.Parent.Id ? null : aggregationEvent.Parent;
                }
            }
        }

        private void ProcessTransformationEvent(TransformationEvent transformationEvent)
        {
            
        }

        private void ProcessObjectEvent(ObjectEvent objectEvent)
        {
            if (objectEvent.Action == EventAction.ADD)
            {
                foreach (var epc in objectEvent.Epcs)
                {
                    epc.Ilmd = objectEvent.Ilmd;
                    _epcRepository.Store(epc);
                }
            }

            if (objectEvent.Action == EventAction.DELETE)
            {
                foreach (var storedEpc in objectEvent.Epcs.Select(epc => _epcRepository.Load(epc.Id)))
                {
                    storedEpc.IsActive = false;
                }
            }
        }
    }
}
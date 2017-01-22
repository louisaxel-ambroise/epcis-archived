using System;
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
            foreach (var @event in events)
            {
                ProcessEvent(@event);

                _eventRepository.Store(@event);
            }
        }

        private void ProcessEvent(BaseEvent @event)
        {
            if (@event.BusinessLocation != null)
                @event.BusinessLocation = _cbvRepository.LoadWithName<BusinessLocation>(@event.BusinessLocation.Name);
            if (@event.BusinessStep != null)
                @event.BusinessStep = _cbvRepository.LoadWithName<BusinessStep>(@event.BusinessStep.Name);
            if (@event.ReadPoint != null)
                @event.ReadPoint = _cbvRepository.LoadWithName<ReadPoint>(@event.ReadPoint.Name);
            if (@event.Disposition != null)
                @event.Disposition = _cbvRepository.LoadWithName<Disposition>(@event.Disposition.Name);

            if (@event is ObjectEvent) ProcessObjectEvent(@event as ObjectEvent);
            if (@event is AggregationEvent) ProcessAggregationEvent(@event as AggregationEvent);
            if (@event is TransactionEvent) ProcessTransactionEvent(@event as TransactionEvent);
            if (@event is TransformationEvent) ProcessTransformationEvent(@event as TransformationEvent);
        }

        private void ProcessAggregationEvent(AggregationEvent aggregationEvent)
        {
            
        }

        private void ProcessTransactionEvent(TransactionEvent transactionEvent)
        {
            
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
                    _epcRepository.Store(epc);
                }
            }
        }
    }
}
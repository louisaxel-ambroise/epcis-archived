using System;
using System.Linq;
using Epcis.Domain.Exceptions;
using Epcis.Domain.Extensions;
using Epcis.Domain.Model.CoreBusinessVocabulary;
using Epcis.Domain.Model.Epcis;

namespace Epcis.Domain.Services.Mapping
{
    // TODO: apply action validations here?
    public class EventMapper : IEventMapper
    {
        public BaseEvent MapEvent(EventParameters parameters)
        {
            switch (parameters.Type.ToUpper())
            {
                case "OBJECTEVENT":
                    return MapObjectEvent(parameters);
                case "TRANSACTIONEVENT":
                    return MapTransactionEvent(parameters);
                case "AGGREGATIONEVENT":
                    return MapAggregationEvent(parameters);
                case "TRANSFORMATIONEVENT":
                    return MapTransformationEvent(parameters);
                case "QUANTITYEVENT":
                    throw new EventMapException("QuantityEvent is deprecated and not supported in this EPCIS implementation.");
                default:
                    throw new EventMapException(string.Format("Event type '{0}' cannot be transformed in EPCIS event", parameters.Type));
            }
        }

        public ObjectEvent MapObjectEvent(EventParameters parameters)
        {
            var action = (EventAction) Enum.Parse(typeof (EventAction), parameters.Action);

            if (action != EventAction.ADD && parameters.Ilmd.Count > 0)
                throw new EventMapException("An OBJECT event with action OBSERVE or DELETE must not contain ILMD");
            if (action == EventAction.ADD && (parameters.Epcs == null || !parameters.Epcs.Any()))
                throw new EventMapException("An OBJECT event with action ADD must have at least one EPC");

            return new ObjectEvent
            {
                Action = action,
                EventTime = DateTime.Parse(parameters.EventTime),
                EventTimeZoneOffset = parameters.EventTimezoneOffset,
                BusinessLocation = parameters.BusinessLocation != null ? new BusinessLocation { Name = parameters.BusinessLocation } : null,
                BusinessStep = parameters.BusinessStep != null ? new BusinessStep { Name = parameters.BusinessStep } : null,
                Disposition = parameters.Disposition != null ? new Disposition { Name = parameters.Disposition } : null,
                ReadPoint = parameters.ReadPoint != null ? new ReadPoint { Name = parameters.ReadPoint } : null,
                Epcs = parameters.Epcs.Select(x => new Epc { Id = x }).ToArray()
            };
        }

        public TransactionEvent MapTransactionEvent(EventParameters parameters)
        {
            var action = (EventAction)Enum.Parse(typeof(EventAction), parameters.Action);

            return new TransactionEvent
            {
                Action = action,
                EventTime = DateTime.Parse(parameters.EventTime),
                EventTimeZoneOffset = parameters.EventTimezoneOffset,
                BusinessLocation = parameters.BusinessLocation != null ? new BusinessLocation { Name = parameters.BusinessLocation } : null,
                BusinessStep = parameters.BusinessStep != null ? new BusinessStep { Name = parameters.BusinessStep } : null,
                Disposition = parameters.Disposition != null ? new Disposition { Name = parameters.Disposition } : null,
                ReadPoint = parameters.ReadPoint != null ? new ReadPoint { Name = parameters.ReadPoint } : null,
            };
        }

        public AggregationEvent MapAggregationEvent(EventParameters parameters)
        {
            var action = (EventAction)Enum.Parse(typeof(EventAction), parameters.Action);

            if((action == EventAction.ADD || action == EventAction.DELETE) && string.IsNullOrEmpty(parameters.ParentId))
                throw new EventMapException("An AGGREGATION event with action ADD or DELETE must have the ParentId property set");
            if(!parameters.ChildEpcs.IsNullOrEmpty() && parameters.ChildQuantityList.IsNullOrEmpty())
                throw new EventMapException("An AGGREGATION event must contain at least one ChildEpc or one ChildQuantityList");

            return new AggregationEvent
            {
                Action = action,
                EventTime = DateTime.Parse(parameters.EventTime),
                EventTimeZoneOffset = parameters.EventTimezoneOffset,
                BusinessLocation = parameters.BusinessLocation != null ? new BusinessLocation { Name = parameters.BusinessLocation } : null,
                BusinessStep = parameters.BusinessStep != null ? new BusinessStep { Name = parameters.BusinessStep } : null,
                Disposition = parameters.Disposition != null ? new Disposition { Name = parameters.Disposition } : null,
                ReadPoint = parameters.ReadPoint != null ? new ReadPoint { Name = parameters.ReadPoint } : null,
            };
        }

        public TransformationEvent MapTransformationEvent(EventParameters parameters)
        {
            if (parameters.InputEpcs.IsNullOrEmpty())
                throw new EventMapException("A TRANSFORMATION event must contain at least one InputEpc");
            if (parameters.OutputEpcs.IsNullOrEmpty())
                throw new EventMapException("A TRANSFORMATION event must contain at least one OutputEpc");

            return new TransformationEvent
            {
                EventTime = DateTime.Parse(parameters.EventTime),
                EventTimeZoneOffset = parameters.EventTimezoneOffset,
                BusinessLocation = parameters.BusinessLocation != null ? new BusinessLocation { Name = parameters.BusinessLocation } : null,
                BusinessStep = parameters.BusinessStep != null ? new BusinessStep { Name = parameters.BusinessStep } : null,
                Disposition = parameters.Disposition != null ? new Disposition { Name = parameters.Disposition } : null,
                ReadPoint = parameters.ReadPoint != null ? new ReadPoint { Name = parameters.ReadPoint } : null,
                InputEpcs = parameters.InputEpcs.Select(x => new Epc { Id = x }).ToArray(),
                OutputEpcs = parameters.OutputEpcs.Select(x => new Epc { Id = x }).ToArray()
            };
        }
    }
}
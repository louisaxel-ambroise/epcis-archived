using FasTnT.Domain.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Web.Models.Events
{
    public static class Mappings
    {
        public static IEnumerable<EventSummaryViewModel> MapToEventSummary(this IQueryable<EpcisEvent> events)
        {
            return events.Select(e => new EventSummaryViewModel
            {
                Id = e.Id,
                Type = e.EventType.ToString("F"),
                Action = e.Action.ToString("F"),
                CaptureTime = e.CaptureTime,
                RecordTime = e.EventTime,
                Location = e.BusinessLocation,
                UserId = default(Guid),
                UserName = null
            });
        }

        public static EventDetailViewModel MapToEventDetail(this EpcisEvent @event)
        {
            return new EventDetailViewModel
            {
                Id = @event.Id,
                CapturedOn = @event.CaptureTime,
                EventTime = @event.EventTime,
                EventTimeZoneOffset = @event.EventTimezoneOffset.Representation,
                EventType = @event.EventType.ToString("F"),
                Action = @event.Action.ToString("F"),
                BizLocation = @event.BusinessLocation,
                BizStep = @event.BusinessStep
            };
        }
    }
}
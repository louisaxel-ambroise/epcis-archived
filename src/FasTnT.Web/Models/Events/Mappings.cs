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
                CaptureTime = e.Request.RecordTime,
                RecordTime = e.EventTime,
                Location = e.BusinessLocation,
                UserId = e.Request.User.Id,
                UserName = e.Request.User.Name
            });
        }

        public static EventDetailViewModel MapToEventDetail(this EpcisEvent @event)
        {
            return new EventDetailViewModel
            {
                Id = @event.Id,
                CaptureTime = @event.Request.RecordTime,
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
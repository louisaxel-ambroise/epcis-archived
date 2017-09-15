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
                Type = e.EventType.ToString(),
                Action = e.Action.ToString(),
                CaptureTime = e.CaptureTime,
                RecordTime = e.EventTime,
                Location = e.BusinessLocation.Id,
                ReadPoint = e.ReadPoint.Id,
                UserId = default(Guid),
                UserName = null
            });
        }

        public static EventDetailViewModel MapToEventDetail(this EpcisEvent @event)
        {
            return new EventDetailViewModel
            {
                Id = @event.Id
            };
        }
    }
}
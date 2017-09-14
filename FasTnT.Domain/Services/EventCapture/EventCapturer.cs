using FasTnT.Domain.Model.Events;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.EventCapture
{
    public class EventCapturer : IEventCapturer
    {
        public IEnumerable<Guid> Capture(IEnumerable<EpcisEvent> events)
        {
            throw new NotImplementedException("Not Implemented Yet");
        }
    }
}
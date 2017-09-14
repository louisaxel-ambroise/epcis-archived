using FasTnT.Domain.Model.Events;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.EventCapture
{
    public interface IEventCapturer
    {
        IEnumerable<Guid> Capture(IEnumerable<EpcisEvent> events);
    }
}
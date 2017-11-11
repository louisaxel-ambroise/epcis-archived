using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.EventCapture
{
    public interface IEventCapturer
    {
        IEnumerable<Guid> Capture(XDocument xmlDocument);
    }
}
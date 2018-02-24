using System.Collections.Generic;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Capture.Events
{
    public interface IEventCapturer
    {
        IEnumerable<string> Capture(XDocument xmlDocument);
    }
}
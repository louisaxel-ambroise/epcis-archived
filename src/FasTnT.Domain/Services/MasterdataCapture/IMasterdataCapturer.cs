using System.Collections.Generic;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.MasterdataCapture
{
    public interface IMasterdataCapturer
    {
        IEnumerable<string> Capture(XDocument document);
    }
}

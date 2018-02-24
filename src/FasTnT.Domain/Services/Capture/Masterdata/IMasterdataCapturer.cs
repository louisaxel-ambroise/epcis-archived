using System.Collections.Generic;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Capture.Masterdata
{
    public interface IMasterdataCapturer
    {
        IEnumerable<string> Capture(XDocument document);
    }
}

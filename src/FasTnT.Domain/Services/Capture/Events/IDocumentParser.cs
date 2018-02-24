using FasTnT.Domain.Model.Events;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Capture.Events
{
    public interface IDocumentParser
    {
        IEnumerable<EpcisEvent> Parse(XElement document);
    }
}

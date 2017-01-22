using System.Xml.Linq;
using Epcis.Domain.Model;
using Epcis.Domain.Model.Epcis;

namespace Epcis.XmlParser.Parsing
{
    public interface IDocumentParser
    {
        BaseEvent[] ParseEvents(XDocument document);
    }
}
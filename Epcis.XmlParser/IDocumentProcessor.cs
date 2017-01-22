using System.Xml.Linq;

namespace Epcis.XmlParser
{
    public interface IDocumentProcessor
    {
        void Process(XDocument document);
    }
}
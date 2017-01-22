using System.Xml.Linq;

namespace Epcis.XmlParser.Validation
{
    public interface IDocumentValidator
    {
        void Validate(XDocument document);
    }
}
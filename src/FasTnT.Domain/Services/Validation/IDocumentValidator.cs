using System.Xml.Linq;

namespace FasTnT.Domain.Services.Validation
{
    public interface IDocumentValidator
    {
        void Validate(XDocument document);
    }
}

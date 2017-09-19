using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace FasTnT.Domain.Services.Validation
{
    public class XmlDocumentValidator : IDocumentValidator
    {
        private readonly XmlSchemaSet _schema;

        public XmlDocumentValidator(string[] files)
        {
            _schema = new XmlSchemaSet();

            foreach(var file in files)
            {
                _schema.Add(null, file);
            }

            _schema.Compile();
        }

        public void Validate(XDocument document)
        {
            document.Validate(_schema, (e, t) => { if (t.Exception != null) throw t.Exception; });
        }
    }

    public interface IDocumentValidator
    {
        void Validate(XDocument document);
    }
}

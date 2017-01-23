using System;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Epcis.XmlParser.Validation
{
    public class DocumentValidator : IDocumentValidator
    {
        private readonly XmlSchemaSet _schemas;

        public DocumentValidator(params string[] xsdFiles)
        {
            if (xsdFiles == null) throw new ArgumentNullException("xsdFiles");
            if(xsdFiles.Length == 0) throw new ArgumentNullException("xsdFiles");

            _schemas = new XmlSchemaSet();

            foreach (var file in xsdFiles)
            {
                _schemas.Add(null, file);
            }
        }

        public void Validate(XDocument document)
        {
            document.Validate(_schemas, (s, e) => { if (e.Exception != null) throw e.Exception; });
        }
    }
}
using System;
using System.Xml.Linq;
using System.Xml.Schema;
using Epcis.Infrastructure.Aop.Log;
using Epcis.Model.Exceptions;

namespace Epcis.Services.Capture.Validation
{
    public class XmlDocumentValidator : IValidator<XDocument>
    {
        private readonly XmlSchemaSet _schema;

        public XmlDocumentValidator(params string[] xsdFiles)
        {
            if (xsdFiles == null) throw new ArgumentNullException("xsdFiles");
            if (xsdFiles.Length == 0) throw new ArgumentNullException("xsdFiles");

            _schema = new XmlSchemaSet();
            foreach (var file in xsdFiles) _schema.Add(null, file);
            _schema.Compile();
        }

        [LogMethodCall]
        public virtual void Validate(XDocument input)
        {
            input.Validate(_schema, (s, e) =>
            {
                if (e.Exception != null)
                {
                    throw new EpcisException("Input document cannot be validated agains EPCIS 1.2 XSD schema", e.Exception);
                }
            });
        }
    }
}
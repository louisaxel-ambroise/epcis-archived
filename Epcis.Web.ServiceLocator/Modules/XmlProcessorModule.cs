using System;
using Epcis.XmlParser;
using Epcis.XmlParser.Parsing;
using Epcis.XmlParser.Validation;
using Ninject.Modules;

namespace Epcis.WebService.ServiceLocator.Modules
{
    public class XmlProcessorModule : NinjectModule
    {
        private readonly string[] _validationFiles;

        public XmlProcessorModule(string[] validationFiles)
        {
            if (validationFiles == null) throw new ArgumentNullException(nameof(validationFiles));

            _validationFiles = validationFiles;
        }

        public override void Load()
        {
            Bind<IDocumentParser>().To<DocumentParser>();
            Bind<IDocumentProcessor>().To<DocumentProcessor>();
            Bind<IDocumentValidator>().To<DocumentValidator>().WithConstructorArgument("xsdFiles", _validationFiles);
        }
    }
}
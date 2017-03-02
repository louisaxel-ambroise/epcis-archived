using System.Collections.Generic;
using System.Xml.Linq;
using Epcis.Model.Vocabularies;

namespace Epcis.Services.Capture.Parsing
{
    public class XmlElementMasterDataParser : IMasterDataParser<XElement>
    {
        public IEnumerable<Vocabulary> Parse(XElement input)
        {
            throw new System.NotImplementedException();
        }
    }
}
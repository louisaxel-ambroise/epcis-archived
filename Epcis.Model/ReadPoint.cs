using System.Xml.Linq;

namespace Epcis.Model
{
    public class ReadPoint
    {
        public string Id { get; set; }
        public XDocument CustomFields { get; set; }
    }
}
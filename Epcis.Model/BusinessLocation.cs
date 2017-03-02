using System.Xml.Linq;

namespace Epcis.Model
{
    public class BusinessLocation
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public XDocument CustomFields { get; set; }
    }
}
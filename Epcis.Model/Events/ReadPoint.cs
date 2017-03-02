using System.Xml.Linq;

namespace Epcis.Model.Events
{
    public class ReadPoint
    {
        public string Id { get; set; }
        public XDocument CustomFields { get; set; }
    }
}
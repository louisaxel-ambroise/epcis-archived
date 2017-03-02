using System.Xml.Linq;

namespace Epcis.Model.Events
{
    public class BusinessLocation
    {
        public string Id { get; set; }
        public XDocument CustomFields { get; set; }
    }
}
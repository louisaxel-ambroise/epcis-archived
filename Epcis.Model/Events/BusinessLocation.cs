using System.Collections.Generic;

namespace Epcis.Model.Events
{
    public class BusinessLocation
    {
        public string Id { get; set; }
        public IList<CustomField> CustomFields { get; set; }
    }
}
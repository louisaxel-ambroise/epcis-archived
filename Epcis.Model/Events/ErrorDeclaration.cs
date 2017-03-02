using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Epcis.Model.Events
{
    public class ErrorDeclaration
    {
        public DateTime DeclarationTime { get; set; }
        public string Reason { get; set; }
        public XDocument CustomFields { get; set; }
        public IList<string> CorrectiveEventIds { get; set; }
    }
}
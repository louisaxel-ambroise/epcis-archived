using System;
using System.Collections.Generic;

namespace Epcis.Model.Events
{
    public class ErrorDeclaration
    {
        public DateTime DeclarationTime { get; set; }
        public string Reason { get; set; }
        public IList<CustomField> CustomFields { get; set; }
        public IList<string> CorrectiveEventIds { get; set; }
    }
}
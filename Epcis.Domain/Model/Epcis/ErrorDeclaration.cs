using System;
using System.Collections.Generic;
using Epcis.Domain.Model.CoreBusinessVocabulary;

namespace Epcis.Domain.Model.Epcis
{
    public class ErrorDeclaration   
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime DeclarationTime { get; set; }
        public virtual ErrorReason ErrorReason { get; set; }
        public virtual IList<EventId> CorrectiveEventIds { get; set; }

        public virtual IDictionary<string, object> Extension { get; set; }
    }
}
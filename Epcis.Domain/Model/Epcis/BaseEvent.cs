using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Epcis.Domain.Model.CoreBusinessVocabulary;

namespace Epcis.Domain.Model.Epcis
{
    public abstract class BaseEvent
    {
        protected BaseEvent()
        {
            RecordTime = DateTime.UtcNow;
        }

        public virtual Guid Id { get; set; }
        public virtual DateTime EventTime { get; set; }
        public virtual string EventTimeZoneOffset { get; set; }
        public virtual DateTime RecordTime { get; set; }
        public virtual EventId EventId { get; set; }

        public virtual BusinessStep BusinessStep { get; set; }
        public virtual BusinessLocation BusinessLocation { get; set; }
        public virtual ReadPoint ReadPoint { get; set; }
        public virtual Disposition Disposition { get; set; }
        public virtual ErrorDeclaration ErrorDeclaration { get; set; }
        public virtual IList<BusinessTransaction> BusinessTransactions { get; set; }
        public virtual IList<SourceDestination> Sources { get; set; }
        public virtual IList<SourceDestination> Destinations { get; set; }

        public virtual XDocument Extension { get; set; }
    }
}
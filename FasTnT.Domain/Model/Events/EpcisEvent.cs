using FasTnT.Domain.Model.MasterData;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Events
{
    public class EpcisEvent
    {
        public virtual Guid Id { get; set; }
        public virtual Guid RequestId { get; set; }
        public virtual string EventId { get; set; }
        public virtual EventType EventType { get; set; }
        public virtual DateTime CaptureTime { get; set; }
        public virtual DateTime EventTime { get; set; }
        public virtual TimeZoneOffset EventTimezoneOffset { get; set; }
        public virtual EventAction Action { get; set; }
        public virtual string TransformationId { get; set; }
        public virtual string ReadPoint { get; set; }
        public virtual string BusinessStep { get; set; }
        public virtual string BusinessLocation { get; set; }
        public virtual string Disposition { get; set; }
        public virtual IList<Epc> Epcs { get; set; } = new List<Epc>();
        public virtual IList<BusinessTransaction> BusinessTransactions { get; set; } = new List<BusinessTransaction>();
        public virtual IList<SourceDestination> SourcesDestinations { get; set; } = new List<SourceDestination>();
        public virtual IList<CustomField> CustomFields { get; set; } = new List<CustomField>();
        public virtual ErrorDeclaration ErrorDeclaration { get; set; }
    }
}

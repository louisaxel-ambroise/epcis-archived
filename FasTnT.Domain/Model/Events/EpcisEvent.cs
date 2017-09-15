using FasTnT.Domain.Model.MasterData;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Events
{
    public class EpcisEvent
    {
        public EpcisEvent()
        {
            Epcs = new List<Epc>();
            BusinessTransactions = new List<BusinessTransaction>();
            SourcesDestinations = new List<SourceDestination>();
            CustomFields = new List<CustomField>();
        }

        public virtual Guid Id { get; set; }
        public virtual string RequestId { get; set; }
        public virtual string EventId { get; set; }
        public virtual EventType EventType { get; set; }
        public virtual DateTime CaptureTime { get; set; }
        public virtual DateTime EventTime { get; set; }
        public virtual TimeZoneOffset EventTimezoneOffset { get; set; }
        public virtual EventAction Action { get; set; }
        public virtual string TransformationId { get; set; }
        public virtual ReadPoint ReadPoint { get; set; }
        public virtual BusinessStep BusinessStep { get; set; }
        public virtual BusinessLocation BusinessLocation { get; set; }
        public virtual Disposition Disposition { get; set; }
        public virtual IList<Epc> Epcs { get; set; }
        public virtual IList<BusinessTransaction> BusinessTransactions { get; set; }
        public virtual IList<SourceDestination> SourcesDestinations { get; set; }
        public virtual IList<CustomField> CustomFields { get; set; }
        public virtual ErrorDeclaration ErrorDeclaration { get; set; }
    }
}

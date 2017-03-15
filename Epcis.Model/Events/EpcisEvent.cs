using System;
using System.Collections.Generic;

namespace Epcis.Model.Events
{
    // TODO: add ILMD
    public class EpcisEvent
    {
        public EpcisEvent()
        {
            BusinessTransactions = new List<BusinessTransaction>();
            Epcs = new List<Epc>();
            SourcesDestinations = new List<SourceDestination>();
            CustomFields = new List<CustomField>();
        }

        public long Id { get; set; }
        public EventType EventType { get; set; }
        public DateTime EventTime { get; set; }
        public DateTime CaptureTime { get; set; }
        public TimeZoneOffset EventTimezoneOffset { get; set; }
        public EventAction? Action { get; set; }
        public string BusinessStep { get; set; }
        public string Disposition { get; set; }
        public string EventId { get; set; }
        // public XDocument Ilmd { get; set; }
        public string TransformationId { get; set; }

        public IList<CustomField> CustomFields { get; set; }
        public BusinessLocation BusinessLocation { get; set; }
        public IList<SourceDestination> SourcesDestinations { get; set; }
        public IList<BusinessTransaction> BusinessTransactions { get; set; }
        public IList<Epc> Epcs { get; set; }
        public ErrorDeclaration ErrorDeclaration { get; set; }
        public ReadPoint ReadPoint { get; set; }
    }
}

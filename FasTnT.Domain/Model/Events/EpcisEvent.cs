using FasTnT.Domain.Model.MasterData;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Events
{
    public class EpcisEvent
    {
        public EpcisEvent()
        {
            CustomFields = new List<CustomField>();
            Epcs = new List<Epc>();
            BusinessTransactions = new List<BusinessTransaction>();
            SourcesDestinations = new List<SourceDestination>();
            CustomFields = new List<CustomField>();
        }

        public Guid Id { get; set; }
        public string RequestId { get; set; }
        public string EventId { get; set; }
        public EventType EventType { get; set; }
        public DateTime CaptureTime { get; set; }
        public DateTime EventTime { get; set; }
        public TimeZoneOffset EventTimezoneOffset { get; set; }
        public EventAction Action { get; set; }
        public string TransformationId { get; set; }
        public ReadPoint ReadPoint { get; set; }
        public BusinessStep BusinessStep { get; set; }
        public BusinessLocation BusinessLocation { get; internal set; }
        public Disposition Disposition { get; set; }
        public IList<Epc> Epcs { get; set; }
        public IList<BusinessTransaction> BusinessTransactions { get; set; }
        public IList<SourceDestination> SourcesDestinations { get; set; }
        public IList<CustomField> CustomFields { get; set; }
        public ErrorDeclaration ErrorDeclaration { get; set; }
    }
}

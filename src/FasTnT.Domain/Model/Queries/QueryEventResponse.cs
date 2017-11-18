using FasTnT.Domain.Model.Events;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Queries
{
    public class QueryEventResponse
    {
        public IEnumerable<EpcisEvent> Events { get; set; }
        public IEnumerable<Epc> Epcs { get; set; }
        public IEnumerable<CustomField> CustomFields { get; set; }
        public IEnumerable<BusinessTransaction> BusinessTransactions { get; set; }
        public IEnumerable<SourceDestination> SourcesDestinations { get; internal set; }
    }
}

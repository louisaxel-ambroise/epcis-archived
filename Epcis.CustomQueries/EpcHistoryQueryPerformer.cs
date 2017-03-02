using System;
using System.Collections.Generic;
using System.Linq;
using Epcis.Data.Queries;
using Epcis.Model.Events;
using Epcis.Model.Queries;
using Epcis.Services.Query.Performers;

namespace Epcis.CustomQueries
{
    public class EpcHistoryQueryPerformer : IQueryPerformer
    {
        private const string SqlQuery = "SELECT epc.EventId FROM epcis.Epc epc WHERE epc.Epc = @epc";
        private readonly IEventsRetriever _eventsRetriever;

        public bool AllowsSubscribe { get { return true; } }
        public string Name { get { return "EpcHistoryQuery"; } }

        public EpcHistoryQueryPerformer(IEventsRetriever eventsRetriever)
        {
            if (eventsRetriever == null) throw new ArgumentNullException("eventsRetriever");

            _eventsRetriever = eventsRetriever;
        }

        public virtual IEnumerable<EpcisEvent> Perform(EpcisQuery query)
        {
            var eventIds = _eventsRetriever.RetrieveIds(SqlQuery, new { Epc = query.Parameters.Single(x => x.Name == "epc").Values.Single() });

            return _eventsRetriever.GetByIds(eventIds.ToArray());
        }
    }
}
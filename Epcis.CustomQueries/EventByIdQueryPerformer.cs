using System;
using System.Collections.Generic;
using System.Linq;
using Epcis.Data.Queries;
using Epcis.Model.Events;
using Epcis.Model.Queries;
using Epcis.Services.Query.Performers;

namespace Epcis.CustomQueries
{
    public class EventByIdQueryPerformer : IQueryPerformer
    {
        private readonly IEventsRetriever _eventsRetriever;

        public bool AllowsSubscribe { get { return false; } }
        public string Name { get { return "EventByIdQuery"; } }

        public EventByIdQueryPerformer(IEventsRetriever eventsRetriever)
        {
            if (eventsRetriever == null) throw new ArgumentNullException("eventsRetriever");

            _eventsRetriever = eventsRetriever;
        }

        public virtual IEnumerable<EpcisEvent> Perform(EpcisQuery query)
        {
            var ids = query.Parameters.Where(x => x.Name == "id").SelectMany(x => x.Values).Select(long.Parse).ToArray();

            return _eventsRetriever.GetByIds(ids);
        }
    }
}

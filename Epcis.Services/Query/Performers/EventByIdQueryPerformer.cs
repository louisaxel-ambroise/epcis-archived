using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Epcis.Data.Queries;
using Epcis.Infrastructure.Aop.Log;
using Epcis.Model;
using Epcis.Model.Exceptions;

namespace Epcis.Services.Query.Performers
{
    public class EventByIdQueryPerformer : IQueryPerformer<XElement>
    {
        private readonly IEventsRetriever _eventsRetriever;

        public EventByIdQueryPerformer(IEventsRetriever eventsRetriever)
        {
            if (eventsRetriever == null) throw new ArgumentNullException("eventsRetriever");

            _eventsRetriever = eventsRetriever;
        }

        public bool CanPerform(string queryName)
        {
            return queryName == "EventByIdQuery";
        }

        [LogMethodCall]
        public virtual IEnumerable<EpcisEvent> Perform(XElement parameters)
        {
            var id = parameters.Elements().ToList();
            if (id == null || !id.Any()) throw new QueryParameterException("Id parameters are expected at this point.");

            return _eventsRetriever.GetByIds(id.Select(x => long.Parse(x.Value)).ToArray());
        }
    }
}
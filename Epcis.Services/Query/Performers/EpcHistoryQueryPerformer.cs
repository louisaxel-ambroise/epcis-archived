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
    public class EpcHistoryQueryPerformer : IQueryPerformer<XElement>
    {
        private readonly IEventsRetriever _eventsRetriever;
        const string SqlQuery = "SELECT evt.* FROM epcis.Event evt JOIN epcis.Epc epc ON epc.EventId = evt.Id WHERE epc.Epc = @epc";

        public EpcHistoryQueryPerformer(IEventsRetriever eventsRetriever)
        {
            if (eventsRetriever == null) throw new ArgumentNullException("eventsRetriever");

            _eventsRetriever = eventsRetriever;
        }

        public bool CanPerform(string queryName)
        {
            return queryName == "EpcHistoryQuery";
        }

        [LogMethodCall]
        public virtual IEnumerable<EpcisEvent> Perform(XElement parameters)
        {
            var epc = parameters.Elements().Single();

            if(epc == null) throw new QueryParameterException("Epc query is expected at this point.");
            
            return _eventsRetriever.Query(SqlQuery, new { Epc = epc.Value });
        }
    }
}
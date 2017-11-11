using System.Collections.Generic;
using System.Linq;
using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Services.EventQuery;
using FasTnT.Domain.Exceptions;

namespace FasTnT.Domain.Services.Queries
{
    public class SimpleEventQuery : IQuery
    {
        public string Name => "SimpleEventQuery";

        public IQueryable<EpcisEvent> ApplyFilter(IQueryable<EpcisEvent> events, QueryParam[] parameters)
        {
            if(parameters.Any())
            {
                ValidateParameters(parameters);
                foreach (var parameter in parameters) events = SimpleEventQueryParameters.ApplyParameter(events, parameter);
            }

            return events;
        }

        public void PerformValidation(IEnumerable<EpcisEvent> events, QueryParam[] parameters)
        {
            var maxEventCount = parameters.SingleOrDefault(param => param.Name == "maxEventCount");

            if(maxEventCount != null && events.Count() > int.Parse(maxEventCount.Value))
                throw new QueryTooComplexException("Number of events greater than maxEventCount");
        }

        private void ValidateParameters(IEnumerable<QueryParam> parameters)
        {
            if (parameters.Any(p => p.Name == "eventCountLimit") && parameters.Any(p => p.Name == "maxEventCount"))
                throw new QueryParameterException("eventCountLimit and maxEventCount are mutually exclusive.");
        }
    }
}

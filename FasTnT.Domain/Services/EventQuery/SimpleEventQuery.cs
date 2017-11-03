using System;
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

        public IQueryable<EpcisEvent> ApplyFilter(IQueryable<EpcisEvent> events, QueryParams parameters)
        {
            if(parameters != null && parameters.Parameters != null)
            { 
                foreach (var parameter in parameters.Parameters) events = SimpleEventQueryParameters.ApplyParameter(events, parameter);
            }

            return events;
        }

        public void PerformValidation(IEnumerable<EpcisEvent> events, QueryParams parameters)
        {
            var maxEventCount = parameters.Parameters.SingleOrDefault(param => param.Name == "maxEventCount");

            if(maxEventCount != null && events.Count() > int.Parse(maxEventCount.Value))
            {
                throw new QueryTooComplexException("Number of events greater than maxEventCount");
            }
        }
    }
}

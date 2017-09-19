using System.Linq;
using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Services.EventQuery;

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
    }
}

using System.Linq;
using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Model.Queries;

namespace FasTnT.Domain.Services.Queries
{
    public class SimpleEventQuery : IQuery
    {
        public string Name => "SimpleEventQuery";

        public IQueryable<EpcisEvent> ApplyFilter(IQueryable<EpcisEvent> events, QueryParams parameters)
        {
            return events;
        }
    }
}

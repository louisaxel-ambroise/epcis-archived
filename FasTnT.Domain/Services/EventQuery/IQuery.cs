using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Model.Queries;
using System.Linq;

namespace FasTnT.Domain.Services.Queries
{
    public interface IQuery
    {
        string Name { get; }
        IQueryable<EpcisEvent> ApplyFilter(IQueryable<EpcisEvent> events, QueryParams parameters);
    }
}

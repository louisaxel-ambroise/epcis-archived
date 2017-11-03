using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Model.Queries;
using System.Linq;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Queries
{
    public interface IQuery
    {
        string Name { get; }
        IQueryable<EpcisEvent> ApplyFilter(IQueryable<EpcisEvent> events, QueryParam[] parameters);
        void PerformValidation(IEnumerable<EpcisEvent> events, QueryParam[] parameters);
    }
}

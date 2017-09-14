using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Model.Queries;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Queries
{
    public interface IQueryPerformer
    {
        IEnumerable<string> ListQueryNames();
        IEnumerable<EpcisEvent> ExecuteQuery(string queryName, QueryParams parameters);
    }
}
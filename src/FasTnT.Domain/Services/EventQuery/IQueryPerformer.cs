using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Model.Queries;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Queries
{
    public interface IQueryPerformer
    {
        IEnumerable<string> ListQueryNames();
        QueryEventResponse ExecuteQuery(string queryName, QueryParam[] parameters);
    }
}
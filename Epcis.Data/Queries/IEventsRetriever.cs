using System.Collections.Generic;
using Epcis.Model.Events;

namespace Epcis.Data.Queries
{
    public interface IEventsRetriever
    {
        IEnumerable<long> RetrieveIds(string query, object parameters);
        IEnumerable<EpcisEvent> Query(string query, object parameters);
        IEnumerable<EpcisEvent> GetByIds(params long[] ids);
    }
}
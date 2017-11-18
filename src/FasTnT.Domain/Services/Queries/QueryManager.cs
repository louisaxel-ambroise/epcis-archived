using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Domain.Services.Queries
{
    public class QueryManager : IQueryManager
    {
        private readonly IQuery[] _queries;

        public QueryManager(params IQuery[] queries)
        {
            _queries = queries;
        }

        public IEnumerable<string> ListQueryNames()
        {
            return _queries.Select(query => query.Name);
        }
    }
}

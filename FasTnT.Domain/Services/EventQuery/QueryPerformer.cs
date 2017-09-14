using System;
using System.Collections.Generic;
using FasTnT.Domain.Model.Queries;
using System.Linq;
using FasTnT.Domain.Model.Events;

namespace FasTnT.Domain.Services.Queries
{
    public class QueryPerformer : IQueryPerformer
    {
        private readonly IQuery[] _queries;

        public QueryPerformer(IQuery[] queries)
        {
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        public IEnumerable<EpcisEvent> ExecuteQuery(string queryName, QueryParams parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ListQueryNames()
        {
            return _queries.Select(x => x.Name);
        }
    }
}
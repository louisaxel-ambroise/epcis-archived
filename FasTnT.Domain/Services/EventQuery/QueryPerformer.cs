using System;
using System.Collections.Generic;
using FasTnT.Domain.Model.Queries;
using System.Linq;
using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Repositories;

namespace FasTnT.Domain.Services.Queries
{
    public class QueryPerformer : IQueryPerformer
    {
        private readonly IEventRepository _eventRepository;
        private readonly IQuery[] _queries;

        public QueryPerformer(IEventRepository eventRepository, IQuery[] queries)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        public IEnumerable<EpcisEvent> ExecuteQuery(string queryName, QueryParams parameters)
        {
            var query = _queries.Single(q => q.Name.Equals(queryName));

            return query.ApplyFilter(_eventRepository.Query(), parameters);
        }

        public IEnumerable<string> ListQueryNames()
        {
            return _queries.Select(x => x.Name);
        }
    }
}
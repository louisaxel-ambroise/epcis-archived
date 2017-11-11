using System;
using System.Collections.Generic;
using FasTnT.Domain.Model.Queries;
using System.Linq;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Exceptions;

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

        public QueryEventResponse ExecuteQuery(string queryName, QueryParam[] parameters)
        {
            var query = _queries.SingleOrDefault(q => q.Name.Equals(queryName)) ?? throw new NoSuchNameException($"Query '{queryName}' does not exist.");
            var events = query.ApplyFilter(_eventRepository.Query(), parameters).ToList();

            query.PerformValidation(events, parameters);

            return new QueryEventResponse
            {
                Events = events,
                BusinessTransactions = _eventRepository.LoadBusinessTransactions(events),
                Epcs = _eventRepository.LoadEpcs(events),
                CustomFields = _eventRepository.LoadCustomFields(events),
                SourcesDestinations = _eventRepository.LoadSourceDestinations(events)
            };
        }

        public IEnumerable<string> ListQueryNames()
        {
            return _queries.Select(x => x.Name);
        }
    }
}
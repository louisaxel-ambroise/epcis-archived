using System;
using System.Collections.Generic;
using System.Linq;
using Epcis.Data.Queries;
using Epcis.Infrastructure.Aop.Log;
using Epcis.Model.Events;
using Epcis.Model.Exceptions;
using Epcis.Model.Queries;

namespace Epcis.Services.Query.Performers
{
    public class SimpleEventQueryPerformer : IQueryPerformer
    {
        private const string SqlQuery = "SELECT * FROM epcis.Event";
        private readonly IEventsRetriever _eventsRetriever;

        public bool AllowsSubscribe { get { return true; } }
        public string Name { get { return "SimpleEventQuery"; } }

        public SimpleEventQueryPerformer(IEventsRetriever eventsRetriever)
        {
            if (eventsRetriever == null) throw new ArgumentNullException("eventsRetriever");

            _eventsRetriever = eventsRetriever;
        }

        [LogMethodCall]
        public virtual IEnumerable<EpcisEvent> Perform(EpcisQuery query)
        {
            var sqlParams = new Dictionary<string, object>();
            var sqlQuery = CreateSqlQuery(query, sqlParams);
            return _eventsRetriever.Query(sqlQuery, sqlParams);
        }

        private static string CreateSqlQuery(EpcisQuery epcisQuery, IDictionary<string, object> sqlParams)
        {
            var sqlFilter = new List<string>();

            foreach (var key in epcisQuery.Parameters)
            {
                switch (key.Name)
                {
                    case "EQ_Something":
                        sqlFilter.Add("Id IN @Id");
                        sqlParams.Add("@Id", key.Values);
                        break;
                    default:
                        throw new QueryParameterException(string.Format("Query parameter {0} is unknown and can't be processed", key));
                }
            }

            return sqlFilter.Any() ? string.Format("{0} WHERE {1}", SqlQuery, string.Join(" AND ", sqlFilter)) : SqlQuery;
        }
    }
}
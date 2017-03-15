using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Epcis.Model.Events;

namespace Epcis.Data.Queries
{
    public class SqlEventsRetriever : IEventsRetriever
    {
        public static string GetEvents = "SELECT evt.* FROM epcis.Event evt WHERE evt.Id IN @Ids";
        public static string GetCustomFields = "SELECT ext.* FROM epcis.EventExtension ext WHERE ext.EventId IN @Ids";
        public static string GetEpcs = "SELECT epc.* FROM epcis.Epc epc WHERE epc.EventId IN @Ids";
        public static string GetTransactions = "SELECT tx.* FROM epcis.BusinessTransaction tx WHERE tx.EventId IN @Ids";
        public static string GetReadPoints = "SELECT rp.* FROM epcis.ReadPoint rp WHERE rp.EventId IN @Ids";
        public static string GetLocations = "SELECT loc.* FROM epcis.BusinessLocation loc WHERE loc.EventId IN @Ids";

        private readonly IDbConnection _connection;

        public SqlEventsRetriever(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public IEnumerable<long> RetrieveIds(string query, object parameters)
        {
            return _connection.Query<long>(query, parameters);
        }

        public IEnumerable<EpcisEvent> Query(string query, object parameters)
        {
            return _connection.Query<EpcisEvent>(query, parameters);
        }

        public IEnumerable<EpcisEvent> GetByIds(params long[] ids)
        {
            var finalQuery = string.Join(" ", GetEvents, GetCustomFields, GetEpcs, GetTransactions, GetReadPoints, GetLocations);
            var related = _connection.QueryMultiple(finalQuery, new { Ids = ids });

            var events = related.Read<EpcisEvent>().ToList();
            var customFields = related.Read().ToList();
            var epcs = related.Read().ToArray();
            var transactions = related.Read().ToArray();
            var readPoints = related.Read().ToArray();
            var locations = related.Read().ToArray();

            foreach (var epcisEvent in events)
            {
                epcisEvent.CustomFields = customFields.Where(x => x.EventId == epcisEvent.Id).Select(ToCustomField).ToList();
                epcisEvent.Epcs = epcs.Where(x => x.EventId == epcisEvent.Id).Select(ToEpc).ToList();
                epcisEvent.BusinessTransactions = transactions.Where(x => x.EventId == epcisEvent.Id).Select(ToBusinessTransaction).ToList();
                epcisEvent.ReadPoint = ToReadPoint(readPoints.SingleOrDefault(x => x.EventId == epcisEvent.Id));
                epcisEvent.BusinessLocation = ToLocation(locations.SingleOrDefault(x => x.EventId == epcisEvent.Id));
            }

            return events;
        }

        private static CustomField ToCustomField(dynamic row)
        {
            return row == null ? null : new CustomField { Namespace = row.Namespace, Name = row.Name, Value = row.Value };
        }

        private static ReadPoint ToReadPoint(dynamic row)
        {
            return row == null ? null : new ReadPoint { Id = row.ReadPointId };
        }

        private static BusinessLocation ToLocation(dynamic row)
        {
            return row == null ? null : new BusinessLocation { Id = row.BusinessLocationId, CustomFields = row.CustomFields };
        }

        private static BusinessTransaction ToBusinessTransaction(dynamic row)
        {
            return new BusinessTransaction { Type = row.TransactionType,  Id = row.TransactionId };
        }

        private static Epc ToEpc(dynamic row)
        {
            return new Epc { Id = row.Epc,  Type = (EpcType)row.Type,  IsQuantity = row.IsQuantity, Quantity = row.Quantity, UnitOfMeasure = row.UnitOfmeasure };
        }
    }
}
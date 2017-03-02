using System;
using System.Data;
using Dapper;
using Epcis.Model.Events;

namespace Epcis.Data.Storage
{
    public class SqlEventStore : IEventStore
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public SqlEventStore(IDbConnection connection, IDbTransaction transaction)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (transaction == null) throw new ArgumentNullException("transaction");

            _connection = connection;
            _transaction = transaction;
        }

        public void Store(EpcisEvent epcisEvent)
        {
            var eventId = StoreEvent(epcisEvent);

            StoreErrorDeclaration(eventId, epcisEvent);
            StoreEpcs(eventId, epcisEvent);
            StoreReadPoint(eventId, epcisEvent);
            StoreBusinessLocation(eventId, epcisEvent);
            StoreBusinessTransactions(eventId, epcisEvent);
            StoreSourcesDestinations(eventId, epcisEvent);
        }

        private long StoreEvent(EpcisEvent epcisEvent)
        {
            var eventId = _connection.QueryFirst<long>(StoreCommands.InsertEvent, epcisEvent.Map(), _transaction);

            return eventId;
        }

        private void StoreErrorDeclaration(long eventId, EpcisEvent epcisEvent)
        {
            if (epcisEvent.ErrorDeclaration == null) return;

            _connection.Execute(StoreCommands.InsertErrorDeclaration, epcisEvent.ErrorDeclaration.Map(eventId), _transaction);
            _connection.Execute(StoreCommands.InsertDeclarationIds, epcisEvent.ErrorDeclaration.MapCorrectiveEventIds(eventId), _transaction);
        }

        private void StoreEpcs(long eventId, EpcisEvent epcisEvent)
        {
            if (epcisEvent.Epcs == null) return;

            _connection.Execute(StoreCommands.InsertEpc, epcisEvent.Epcs.Map(eventId), _transaction);
        }

        private void StoreReadPoint(long eventId, EpcisEvent epcisEvent)
        {
            if (epcisEvent.ReadPoint == null) return;

            _connection.Execute(StoreCommands.InsertReadPoint, epcisEvent.ReadPoint.Map(eventId), _transaction);
        }

        private void StoreBusinessLocation(long eventId, EpcisEvent epcisEvent)
        {
            if (epcisEvent.BusinessLocation == null) return;

            _connection.Execute(StoreCommands.InsertBusinessLocation, epcisEvent.BusinessLocation.Map(eventId), _transaction);
        }

        private void StoreBusinessTransactions(long eventId, EpcisEvent epcisEvent)
        {
            if (epcisEvent.BusinessTransactions == null) return;

            _connection.Execute(StoreCommands.InsertBusinessTransaction, epcisEvent.BusinessTransactions.Map(eventId), _transaction);
        }

        private void StoreSourcesDestinations(long eventId, EpcisEvent epcisEvent)
        {
            if (epcisEvent.SourcesDestinations == null) return;

            _connection.Execute(StoreCommands.InsertBusinessSourceDest, epcisEvent.SourcesDestinations.Map(eventId), _transaction);
        }
    }
}

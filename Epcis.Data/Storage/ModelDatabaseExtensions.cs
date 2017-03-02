using System;
using System.Collections.Generic;
using System.Linq;
using Epcis.Model.Events;

namespace Epcis.Data.Storage
{
    // Maps the model entities to Dapper correct objects
    public static class ModelDatabaseExtensions
    {
        public static object Map(this EpcisEvent epcisEvent)
        {
            return new
            {
                EventType = (int)epcisEvent.EventType,
                epcisEvent.EventTime,
                CaptureTime = DateTime.UtcNow,
                EventTimezoneOffset = epcisEvent.EventTimezoneOffset.Value,
                Action = epcisEvent.Action != null ? (int?)epcisEvent.Action : null,
                epcisEvent.BusinessStep,
                epcisEvent.Disposition,
                epcisEvent.EventId,
                epcisEvent.Ilmd,
                epcisEvent.TransformationId,
                epcisEvent.CustomFields
            };
        }

        public static IEnumerable<object> Map(this IEnumerable<Epc> epcs, long eventId)
        {
            return epcs.Select(x => new
            {
                EventId = eventId,
                Epc = x.Id,
                x.Type,
                x.IsQuantity,
                x.Quantity,
                x.UnitOfMeasure
            });
        }

        public static object Map(this ErrorDeclaration errorDeclaration, long eventId)
        {
            return new
            {
                EventId = eventId,
                errorDeclaration.DeclarationTime,
                errorDeclaration.Reason,
                errorDeclaration.CustomFields
            };
        }

        public static IEnumerable<object> MapCorrectiveEventIds(this ErrorDeclaration errorDeclaration, long eventId)
        {
            return errorDeclaration.CorrectiveEventIds.Select(x => new
            {
                EventId = eventId,
                ReferencedId = x
            });
        }

        public static object Map(this ReadPoint readPoint, long eventId)
        {
            return new
            {
                EventId = eventId,
                ReadPointId = readPoint.Id,
                readPoint.CustomFields
            };
        }

        public static object Map(this BusinessLocation location, long eventId)
        {
            return new
            {
                EventId = eventId,
                BusinessLocationId = location.Id,
                location.CustomFields
            };
        }

        public static IEnumerable<object> Map(this IEnumerable<BusinessTransaction> transactions, long eventId)
        {
            return transactions.Select(x => new
            {
                EventId = eventId,
                TransactionId = x.Id,
                x.Type
            });
        }

        public static IEnumerable<object> Map(this IEnumerable<SourceDestination> sourceDests, long eventId)
        {
            return sourceDests.Select(x => new
            {
                EventId = eventId,
                SourceDestId = x.Id,
                x.Direction,
                x.Type
            });
        }
    }
}
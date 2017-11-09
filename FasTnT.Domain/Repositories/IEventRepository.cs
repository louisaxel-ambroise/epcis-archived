using FasTnT.Domain.Model.Events;
using System;
using System.Linq;
using System.Collections.Generic;
using FasTnT.Domain.Model;

namespace FasTnT.Domain.Repositories
{
    public interface IEventRepository
    {
        IQueryable<EpcisEvent> Query();
        EpcisEvent LoadById(Guid eventId);
        IEnumerable<BusinessTransaction> LoadBusinessTransactions(IEnumerable<EpcisEvent> events);
        IEnumerable<Epc> LoadEpcs(IEnumerable<EpcisEvent> events);
        IEnumerable<CustomField> LoadCustomFields(IEnumerable<EpcisEvent> events);
        IEnumerable<SourceDestination> LoadSourceDestinations(IEnumerable<EpcisEvent> events);
    }
}

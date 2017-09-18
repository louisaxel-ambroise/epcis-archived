using FasTnT.Domain.Model.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FasTnT.Domain.Repositories
{
    public interface IEventRepository
    {
        IQueryable<EpcisEvent> Query();
        EpcisEvent LoadById(Guid eventId);
        void Insert(EpcisEvent @event);
    }
}

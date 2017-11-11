using FasTnT.Domain.Model.Events;
using FasTnT.Domain.Repositories;
using NHibernate;
using System;

namespace FasTnT.Data.Repositories
{
    public class EpcisRequestRepository : IEpcisRequestRepository
    {
        private readonly ISession _session;

        public EpcisRequestRepository(ISession session)
        {
            _session = session ?? throw new ArgumentException(nameof(session));
        }

        public void Insert(EpcisRequest request)
        {
            _session.Persist(request);
        }
    }
}

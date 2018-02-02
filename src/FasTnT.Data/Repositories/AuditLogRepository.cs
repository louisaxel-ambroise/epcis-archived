using FasTnT.Domain.Repositories;
using System;
using System.Linq;
using FasTnT.Domain.Model.Log;
using NHibernate;
using NHibernate.Linq;

namespace FasTnT.Data.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ISession _session;

        public AuditLogRepository(ISession session)
        {
            _session = session;
        }

        public AuditLog Load(Guid id)
        {
            return _session.Load<AuditLog>(id);
        }

        public IQueryable<AuditLog> Query()
        {
            return _session.Query<AuditLog>();
        }
    }
}

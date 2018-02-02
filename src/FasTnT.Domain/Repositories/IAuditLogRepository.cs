using FasTnT.Domain.Model.Log;
using System;
using System.Linq;

namespace FasTnT.Domain.Repositories
{
    public interface IAuditLogRepository
    {
        AuditLog Load(Guid id);
        IQueryable<AuditLog> Query();
    }
}
